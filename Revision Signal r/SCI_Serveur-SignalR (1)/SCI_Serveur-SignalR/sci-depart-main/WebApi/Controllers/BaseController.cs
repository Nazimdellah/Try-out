using System;
using Microsoft.AspNetCore.Mvc;
using Super_Cartes_Infinies.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Super_Cartes_Infinies.Services;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public PlayersService playersService;

        public BaseController(PlayersService playersService)
        {
            this.playersService = playersService;
        }

        private Player? player = null;

        public Player CurrentPlayer
        {
            get
            {
                if(player == null)
                {
                    player = playersService.GetPlayerFromUserId(UserId);
                }
                return player;
            }
        }

        public string UserId
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier)!; ;
            }
        }
    }
}

