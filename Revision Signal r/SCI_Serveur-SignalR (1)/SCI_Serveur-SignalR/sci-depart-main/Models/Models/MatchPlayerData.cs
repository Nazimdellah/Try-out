using Models.Interfaces;

namespace Super_Cartes_Infinies.Models
{
	public class MatchPlayerData : IModel
    {
		private const int STARTING_HEALTH = 20;

        public MatchPlayerData()
        {
        }

        // Utilisé par les tests
        public MatchPlayerData(int playerId)
		{
            PlayerId = playerId;
            Health = STARTING_HEALTH;
            CardsPile = new List<PlayableCard>();
            Hand = new List<PlayableCard>();
            BattleField = new List<PlayableCard>();
            Graveyard = new List<PlayableCard>();
        }

        public MatchPlayerData(Player p, IEnumerable<Card> cardList) : this(p.Id)
        {
            // TODO: Lors de l'intégration, remplacer par les cartes du joueur, on n'aura plus besoin de la liste de cartes
            foreach (var card in cardList) {
                CardsPile.Add(new PlayableCard(card));
            }
        }

        public int Id { get; set; }
		public int Health { get; set; }
        public int Mana { get; set; }

        public virtual Player Player { get; set; }
        public int PlayerId { get; set; }

        // TODO: Il faut ordonner correctement toutes ces listes/stacks qui pourraient avoir un ordre différent quand on les obtient de la BD (mettre des indexes partout...)
        public virtual List<PlayableCard> CardsPile { get; set; }
        public virtual List<PlayableCard> Hand { get; set; }

        public virtual List<PlayableCard> BattleField { get; set; }
        public virtual List<PlayableCard> Graveyard { get; set; }
    }
}

