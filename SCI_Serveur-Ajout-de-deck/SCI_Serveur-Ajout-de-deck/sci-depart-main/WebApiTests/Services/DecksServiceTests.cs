using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Super_Cartes_Infinies.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Super_Cartes_Infinies.Models;

namespace DeckService.Tests
{
    [TestClass()]
    public class DecksServiceTests

    {
        private ApplicationDbContext _db;
        [TestInitialize]
        public void Init()
        {
            // En utilisant un nom différent à chaque fois, on n'a pas besoin de retirer les données
            string dbName = "DeckService" + Guid.NewGuid().ToString();
            // TODO On initialise les options de la BD, on utilise une InMemoryDatabase
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                // TODO il faut installer la dépendance Microsoft.EntityFrameworkCore.InMemory
                .UseInMemoryDatabase(databaseName: dbName)
                .UseLazyLoadingProxies(true) // Active le lazy loading
                .Options;

            _db = new ApplicationDbContext(options);
            Deck[] deck = new Deck[] {
    new Deck
    {
        Id = 1,
        Name = "Deck 1",
        Courant = true,
        PlayerId = 1,
        CarteJoueurs = new List<OwnedCard>(),
        CarteSuprime = new List<OwnedCard>()
    },
    new Deck
    {
        Id = 2,
        Name = "Deck 3",
        Courant = false,
        PlayerId = 1,
        CarteJoueurs = new List<OwnedCard>(),
        CarteSuprime = new List<OwnedCard>()
    },
     new Deck
    {
        Id = 3,
        Name = "Deck 3",
        Courant = false,
        PlayerId = 1,
        CarteJoueurs = new List<OwnedCard>(),
        CarteSuprime = new List<OwnedCard>()
    },
};

            _db.AddRange(deck);
            _db.SaveChanges();

        }
        [TestCleanup]
        public void Dispose()
        {
            _db.Dispose();
}
        
      
       


        [TestMethod()]
        public void AjoutDeckTest()
        {
          DecksService decksService = new DecksService(_db, null);
            Deck deckAjouter = new Deck();
            deckAjouter.Name = "Deck 2";
            deckAjouter.Courant = false;
            deckAjouter.PlayerId = 1;
            decksService.AjoutDeck("Deck 2", 1);

            Assert.AreEqual(deckAjouter.Name, "Deck 2");
            Assert.AreEqual(deckAjouter.Courant, false);
            Assert.AreEqual(deckAjouter.PlayerId, 1);
        }

       
        [TestMethod()]
        public void DeleteDeckTest()
        {
            DecksService decksService = new DecksService(_db, null);
            decksService.DeleteDeck(_db.Decks.FirstOrDefault(d => d.Id == 2));
            Assert.IsNull(_db.Decks.FirstOrDefault(d => d.Id == 2));

        }
        [TestMethod]
        public void deleteDeckAuthorise()
        {
            DecksService decksService = new DecksService(_db, null);
            Player player = new Player
            {
                Id = 3,
                UserId = "user1",
                listeDeck = new List<Deck>
                {
                    new Deck { Id = 4, Name = "Deck 1", Courant = true },
                    new Deck { Id = 5, Name = "Deck 2", Courant = false }
                }
            };
            
            
                decksService.DeletePlayerDeck(3,4); 
            Assert.IsNotNull(_db.Decks.FirstOrDefault(d => d.Id == 3));
            
            






        }


        [TestMethod()]
        public async Task AddPlayerCard_AjouteCarteSiValide()
        {
            // Arrange
            var decksService = new DecksService(_db, null);

            // Ajout d'une carte
            var card = new Card { Id = 9, Name = "Carte Test" };
            await _db.Cards.AddAsync(card);

            // Ajout d'une config de jeu avec max 5 cartes
            var config = new GameConfig { nbMaxCartesDecks = 5 };
            await _db.GameConfig.AddAsync(config);

            // Ajout d'un deck appartenant au joueur 9
            var deck = new Deck
            {
                Id = 9,
                Name = "Deck Test",
                Courant = true,
                PlayerId = 9,
                CarteJoueurs = new List<OwnedCard>(),
                CarteSuprime = new List<OwnedCard>()
            };
            await _db.Decks.AddAsync(deck);

            await _db.SaveChangesAsync();

            // Act
            var resultDeck = await decksService.addplayerCard(9, 9, 9);

            // Assert
            Assert.IsNotNull(resultDeck);
            Assert.AreEqual(1, resultDeck.CarteJoueurs.Count);
            Assert.AreEqual("Carte Test", resultDeck.CarteJoueurs.First().Card.Name);
        }
        [TestMethod()]
        public async Task DeletePlayerCarte_DeplaceCarteDansCarteSupprime()
        {
            // Arrange
            var decksService = new DecksService(_db, null);

            // Création de la carte
            var card = new Card { Id = 10, Name = "Carte Test" };
            await _db.Cards.AddAsync(card);

            // Création de l'OwnedCard (appartient déjà au deck)
            var ownedCard = new OwnedCard { Id = 10, Card = card };
            await _db.OwnedCard.AddAsync(ownedCard);

            // Création du deck avec l'OwnedCard
            var deck = new Deck
            {
                Id = 10,
                Name = "Deck Test",
                Courant = true,
                PlayerId = 10,
                CarteJoueurs = new List<OwnedCard> { ownedCard },
                CarteSuprime = new List<OwnedCard>()
            };
            await _db.Decks.AddAsync(deck);

            await _db.SaveChangesAsync();

            // Act
            var resultDeck = await decksService.DeleteplayerCarte(10, 10, 10);

            // Assert
            Assert.IsNotNull(resultDeck);
            Assert.AreEqual(0, resultDeck.CarteJoueurs.Count);
            Assert.AreEqual(1, resultDeck.CarteSuprime.Count);
            Assert.AreEqual("Carte Test", resultDeck.CarteSuprime.First().Card.Name);
        }



    }
}