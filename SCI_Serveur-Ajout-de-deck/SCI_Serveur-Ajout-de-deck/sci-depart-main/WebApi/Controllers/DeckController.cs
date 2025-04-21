using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Super_Cartes_Infinies.Controllers;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;
using Super_Cartes_Infinies.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class Temp
    {
        public string Name { get; set; }
        public int PlayerId { get; set; }
    }
  

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DeckController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private CardsService _cardsService;
        private DecksService _deckService;
        [HttpPost]
        public async Task<ActionResult<Deck>> CreateDeck(Temp objet)
        {
           
            return Ok(await _deckService.AjoutDeck(objet.Name, objet.PlayerId));

        }
        [HttpGet("{playerId}")]
        public async Task<ActionResult> GetDeck(int playerId)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(await _deckService.getDeck(playerId));
        }

        [HttpGet]
        public async Task<ActionResult<List<Card>>> GetAllCard()
        {
            return Ok(await _deckService.GetAllCards());
        }

        [HttpPost]
        public async Task<ActionResult>AjoutDcarte(int DeckID, int cardID)
        {
            return Ok(await _deckService.AddCarte(DeckID,  cardID));
        }

        public DeckController(ApplicationDbContext dbContext, DecksService decksService)
        {
            _dbContext = dbContext;
            _deckService = decksService;
        }
        [HttpDelete]
        public async Task<ActionResult<Deck>> DeleteCarte(int deckID , int OwnedCardId)
        {

            return Ok (await _deckService.DeleteCarte(deckID, OwnedCardId));
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Deletedeck(int deckID)
        {
            await _deckService.Deletedeck(deckID);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Deck>> SetCourantDeck(int deckID, int PlayerID)
        {
            return Ok(await _deckService.SetCourantDeck(deckID, PlayerID));
        }

    }
  
}
