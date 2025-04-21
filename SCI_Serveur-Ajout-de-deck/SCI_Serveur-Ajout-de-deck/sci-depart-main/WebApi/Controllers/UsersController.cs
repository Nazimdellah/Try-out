using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.Models.Dtos;
using Super_Cartes_Infinies.Models;
using Super_Cartes_Infinies.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVCEtWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController: ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly PlayersService _playersService;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, PlayersService playersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _playersService = playersService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Le mot de passe et la confirmation ne sont pas identiques" });
            }

            IdentityUser user = new IdentityUser()
            {
                UserName = registerDTO.Email,

                Email = registerDTO.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = identityResult.Errors });
            }

            await _playersService.CreatePlayer(user);
            


            return Ok(true);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C'est tellement la meilleure cle qui a jamais ete cree dans l'histoire de l'humanite (doit etre longue)"));
                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7179",
                    audience: null,
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                );

                Player player = _playersService.GetPlayerFromUserId(user.Id);

                return Ok(new LoginSuccessDTO { Token = new JwtSecurityTokenHandler().WriteToken(token), Email = user.Email, PlayerId = player.UserId, PlayerNumId = player.Id });
            }

            return NotFound(new { Error = "L'utilisateur est introuvable ou le mot de passe ne concorde pas" });

        }
        [Authorize]
        [HttpGet]
        public ActionResult<string[]> PrivateData()
        {
            return new string[] { "figue", "banane", "noix" };
        }
    }
}
