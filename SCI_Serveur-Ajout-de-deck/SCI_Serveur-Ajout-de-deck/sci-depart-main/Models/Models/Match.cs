using Models.Interfaces;
using Models.Models;

namespace Super_Cartes_Infinies.Models
{
	public class Match : IModel
    {
		public Match()
		{
		}

        // Pour créer un nouveau match pour 2 joueurs
        public Match(Player playerA, Player playerB, IEnumerable<Card> cardsPlayerA, IEnumerable<Card> cardsPlayerB)
        {
            Id = 0;
            IsMatchCompleted = false;
            UserAId = playerA.UserId;
            PlayerDataA = new MatchPlayerData(playerA, cardsPlayerA);
            UserBId = playerB.UserId;
            PlayerDataB = new MatchPlayerData(playerB, cardsPlayerB);
        }

        public int Id { get; set; }
        
        public bool IsPlayerATurn { get; set; } = false;

        public bool IsMatchCompleted { get; set; } = false;


        // Ici on garde simplement un copie des UserIds et non une véritable référence vers les IdentityUser
        // C'est simplement pour réduire la complexité du modèle de données EntitfyFramework
        // Lorsque les relations deviennent plus complexes, on doit éventuellement utilisé Fluent API.
        // https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties
        // Nous allons couvrir ce sujet plus tard dans la session
        public string? WinnerUserId { get; set; }
        public string UserAId { get; set; }
        public string UserBId { get; set; }
        public virtual MatchPlayerData PlayerDataA { get; set; }
        public virtual MatchPlayerData PlayerDataB { get; set; }
    }
}

