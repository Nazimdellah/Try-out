using Microsoft.EntityFrameworkCore;
using Models.Models;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;
using System.Collections.Generic;
using WebApi.Services;

namespace Super_Cartes_Infinies.Services
{
	public class CardsService
    {
        private ApplicationDbContext _dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Card> GetPlayersCards(string userId)
        {
            // Récupérer les cartes possédées par le joueur en utilisant l'ID de l'utilisateur
            var player = _dbContext.Players.FirstOrDefault(p => p.UserId == userId);
            if (player == null)
            {
                return new List<Card>();
            }

            var ownedCards = _dbContext.OwnedCard
                .Where(oc => oc.Player.Id == player.Id)
                .Select(oc => oc.Card)
                .ToList();

            return ownedCards;
        }

        public async Task<IEnumerable<Card>> GetUserDeckCards(int playerId)
        {
            Deck deck = await _dbContext.Decks.Where(d => d.PlayerId == playerId && d.Courant).FirstOrDefaultAsync();
            List<Card> listCARTE = new List<Card>();

            if (deck == null)
                throw new NullReferenceException();

            foreach (OwnedCard ownedCard in deck.CarteJoueurs)
            {
                listCARTE.Add(ownedCard.Card);
            }
            return listCARTE;


            //foreach (Deck deck in deckList)
            //{
            //    if (deck.Courant)
            //    {
            //        foreach (OwnedCard ownedCard in deck.CarteJoueurs)
            //        {
            //            listCARTE.Add(ownedCard.Card);
            //        }
            //        return listCARTE;
            //    }
            //}
        }
        public IEnumerable<Card> GetAllCards()
        {
            return _dbContext.Cards;
        }
    }
}

