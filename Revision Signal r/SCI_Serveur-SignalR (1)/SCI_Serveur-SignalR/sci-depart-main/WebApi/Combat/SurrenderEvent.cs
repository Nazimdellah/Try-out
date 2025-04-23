using Super_Cartes_Infinies.Models;
using System.Text.Json.Serialization;

namespace Super_Cartes_Infinies.Combat
{
    public class SurrenderEvent : MatchEvent
    {
        public override string EventType { get { return "Surrender"; } }
        public int SurrenderingPlayerId { get; set; }

        // L'évènement lorsqu'un joueur joue une carte
        public SurrenderEvent(Match match, MatchPlayerData surrenderingPlayerData, MatchPlayerData opposingPlayerData)
        {
            SurrenderingPlayerId = surrenderingPlayerData.Player.Id;
            this.Events = new List<MatchEvent>()
            {
                new EndMatchEvent(match, opposingPlayerData, surrenderingPlayerData)
            };
        }
    }
}
