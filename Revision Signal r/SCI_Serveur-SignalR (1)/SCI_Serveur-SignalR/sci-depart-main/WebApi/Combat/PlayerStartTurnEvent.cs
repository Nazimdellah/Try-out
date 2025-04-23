using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Combat
{
    public class PlayerStartTurnEvent : MatchEvent
    {
        public override string EventType { get { return "PlayerStartTurn"; } }
        public int PlayerId { get; set; }
        // L'évènement lorsqu'un joueur débutte son tour
        public PlayerStartTurnEvent(MatchPlayerData playerData, int nbManaPerTurn)
        {
            this.PlayerId = playerData.PlayerId;
            this.Events = new List<MatchEvent>();

            // TODO: Faire piger UNE carte (celle qui est pigé à chaque début de tour)
            // TODO: Faire gagner le Mana selon la configuration

            Events.Add(new DrawCardEvent(playerData));
            Events.Add(new GainManaEvent(playerData, nbManaPerTurn));
        }

    }
}
