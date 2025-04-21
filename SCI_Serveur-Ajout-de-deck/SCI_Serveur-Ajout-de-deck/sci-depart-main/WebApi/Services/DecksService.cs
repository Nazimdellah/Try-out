using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Client;
using Models.Models;
using Super_Cartes_Infinies.Data;
using Super_Cartes_Infinies.Models;
using Super_Cartes_Infinies.Services;
using System.Collections;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace WebApi.Services
{
    public class DecksService
        
    {
       

        private ApplicationDbContext _dbContext;
        private CardsService _cardsService;
        

        public DecksService(ApplicationDbContext dbContext , CardsService cardsService)
        {
            _dbContext = dbContext;
            _cardsService = cardsService;
        }

        public async Task <Deck> AjoutDeck(string name,int playerId )
        {
            Player player = await _dbContext.Players.SingleOrDefaultAsync(p=>p.Id == playerId);
            var nbDeck =  await _dbContext.GameConfig.Select(x=>x.nbMaxDecks).FirstOrDefaultAsync();
            Deck newDeck = new Deck();
            if (player.listeDeck.Count() < nbDeck)
            {
               
                newDeck.Name = name;
                newDeck.Courant = false;
                newDeck.PlayerId = playerId;


                player.listeDeck.Add(newDeck);
                _dbContext.Decks.Add(newDeck);
                await _dbContext.SaveChangesAsync();
                

            }
            return newDeck;
            

           
        }
        public async Task<IEnumerable<Deck>> getDeck(int playerId)
        {
            List<Deck> deck = await _dbContext.Decks.Where(d => d.PlayerId == playerId).ToListAsync();
            return deck;
        }
        public async Task DeleteDeck(Deck deck)
        {
            if (deck.Courant!=true)
            {
                _dbContext.Remove(deck);
                _dbContext.SaveChangesAsync();
            }
        }

        public async Task <Deck>  AddCarte(int DeckID, int cardID)

        {
            Card card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.Id == cardID);
            var maxCartes = await _dbContext.GameConfig.Select(x => x.nbMaxCartesDecks).FirstOrDefaultAsync();

            Deck deckCourant = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == DeckID);

            OwnedCard ownedCard = new OwnedCard();
            if (deckCourant.CarteJoueurs.Count()<maxCartes)
            {
              
                ownedCard.Card = card;
               deckCourant.CarteJoueurs.Add(ownedCard);
                var x = deckCourant.CarteSuprime.FirstOrDefault(carte => carte.Card.Id == cardID);
                if (x != null)
                {
                    deckCourant.CarteSuprime.Remove(x);

                }
                

            }
            await _dbContext.OwnedCard.AddAsync(ownedCard);
            await _dbContext.SaveChangesAsync();



            return deckCourant;




        }
        public async Task <Deck> DeleteCarte(int DeckID , int ownedCardId)
        {
           _dbContext.SaveChanges();
            Deck deckCourant = await _dbContext.Decks.FirstOrDefaultAsync(d=>d.Id ==DeckID);
            OwnedCard carteAsuprrimé = await _dbContext.OwnedCard.FirstOrDefaultAsync(d => d.Id == ownedCardId);
            deckCourant.CarteJoueurs.Remove(carteAsuprrimé);
            var x = deckCourant.CarteSuprime.FirstOrDefault(x => x.Card.Id == carteAsuprrimé.Card.Id);

            if (x == null)
            {
                deckCourant.CarteSuprime.Add(carteAsuprrimé);
            }
           
            await _dbContext.SaveChangesAsync();

            return deckCourant;
           


        }
        public async Task Deletedeck(int deckID)
        {
            //_dbContext.SaveChanges();
            Deck deck = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == deckID);
            if (deck.Courant != true)
            {
                _dbContext.Remove(deck);
                await _dbContext.SaveChangesAsync();
            }
        }   
        public async Task<Deck> SetCourantDeck(int deckID, int PlayerID)

        {
            _dbContext.SaveChanges();
           
            var userDecks = await _dbContext.Decks.Where(d => d.PlayerId == PlayerID).ToListAsync();
            Deck deck = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == deckID);
            if (deck.Courant != true)
            {
               
                foreach (var item in userDecks)
                {
                    if (item.Courant)
                    {
                        item.Courant = false;
                        deck.Courant = true;
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            return deck;
           
        }
        public async Task<Deck> DeletePlayerDeck(int deckId,int playerId) {
            Deck deck = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == deckId);
            if (deck.PlayerId==playerId)
            {
                _dbContext.Remove(deck);
                await _dbContext.SaveChangesAsync();
            }
            return deck;
           
        }
        public async Task<Deck> addplayerCard(int carteID ,int playerId,int deckID)
        {
            Card card = await _dbContext.Cards.FirstOrDefaultAsync(c => c.Id == carteID);
            var maxCartes = await _dbContext.GameConfig.Select(x => x.nbMaxCartesDecks).FirstOrDefaultAsync();
            
            Deck deckCourant = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == deckID);
            if (deckCourant.PlayerId==playerId)
            {
                OwnedCard ownedCard = new OwnedCard();
                if (deckCourant.CarteJoueurs.Count() <= maxCartes)
                {

                    ownedCard.Card = card;
                    deckCourant.CarteJoueurs.Add(ownedCard);



                }
                await _dbContext.OwnedCard.AddAsync(ownedCard);
                await _dbContext.SaveChangesAsync();
            

            }
            return deckCourant;


        }
        public async Task<Deck> DeleteplayerCarte(int DeckID, int ownedCardId,int playerID)
        {
            Deck deckCourant = await _dbContext.Decks.FirstOrDefaultAsync(d => d.Id == DeckID);
            if (deckCourant.PlayerId==playerID)
            {
                OwnedCard carteAsuprrimé = await _dbContext.OwnedCard.FirstOrDefaultAsync(d => d.Id == ownedCardId);
                deckCourant.CarteJoueurs.Remove(carteAsuprrimé);
                deckCourant.CarteSuprime.Add(carteAsuprrimé);
                await _dbContext.SaveChangesAsync();
            }
          

            return deckCourant;


           
        }
        public async Task<List<Card>> GetAllCards()
        {
            return await _dbContext.Cards.ToListAsync();
        }








    }



}
    

