using Microsoft.Extensions.Logging;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Combat
{
    public class GainManaEvent : MatchEvent
    {
        public override string EventType { get { return "GainMana"; } }
        public int Mana { get; set; }
        public int PlayerId { get; set; }

        public GainManaEvent(MatchPlayerData playerData, int mana)
        {
            Mana = mana;
            PlayerId = playerData.PlayerId;
            playerData.Mana += mana;
        }
    }
}
