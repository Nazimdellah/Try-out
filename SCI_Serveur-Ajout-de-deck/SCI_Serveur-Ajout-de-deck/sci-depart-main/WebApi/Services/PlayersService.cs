using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;
using System.Numerics;

namespace Super_Cartes_Infinies.Services
{
	public class PlayersService
    {
        private ApplicationDbContext _dbContext;
        private readonly StartingCardsService _startingCard;

        public PlayersService(ApplicationDbContext context,StartingCardsService startingcardService)
        {
            _dbContext = context;
            _startingCard = startingcardService;
        }

        public async Task<Player> CreatePlayer(IdentityUser user)
        {
            Player player = new Player() { UserId = user.Id, Name = user.Email! };

            // TODO: Utilisez le service StartingCardsService pour obtenir les cartes de départ
            // TODO: Ajoutez ces cartes au joueur en utilisant le modèle OwnedCard que vous allez devoir ajouter
            await _dbContext.Players.AddAsync(player);
            Deck deck = new Deck();
            deck.Name = "Depart";
            deck.PlayerId = player.Id;
            deck.Player = player;
            deck.Courant = true;
            deck.CarteJoueurs = new List<OwnedCard>();
            player.listeDeck = new List<Deck> ();

            player.listeDeck.Add(deck);
           
         

            List<StartingCard> startingCards = await _startingCard.GetStartingCards();

            foreach (var startingCard in startingCards)
            {
                OwnedCard ownedCard = new OwnedCard() { Card = startingCard.Card, Player = player };
               
                await _dbContext.OwnedCard.AddAsync(ownedCard);
                deck.CarteJoueurs.Add(ownedCard);
               
            }
            
            await _dbContext.Decks.AddAsync(deck);
          
            
          
            await _dbContext.SaveChangesAsync();
            return player;
        }

        public async Task AddDeckDepart(string userId)
        {

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

