using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Services
{
	public class PlayersService
    {
        private ApplicationDbContext _dbContext;

        public PlayersService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public Player CreatePlayer(IdentityUser user)
        {
            Player p = new Player()
            {
                Id = 0,
                UserId = user.Id,
                Name = user.Email!
            };

            // TODO: Utilisez le service StartingCardsService pour obtenir les cartes de départ
            // TODO: Ajoutez ces cartes au joueur en utilisant le modèle OwnedCard que vous allez devoir ajouter

            _dbContext.Add(p);
            _dbContext.SaveChanges();

            return p;
        }

        public virtual Player GetPlayerFromUserId(string userId)
        {
            return _dbContext.Players.Single(p => p.UserId == userId);
        }

        public Player GetPlayerFromUserName(string userName)
        {
            return _dbContext.Players.Single(p => p.User!.UserName == userName);
        }
    }
}

