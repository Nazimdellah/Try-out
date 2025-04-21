using System.Text.Json.Serialization;

namespace Super_Cartes_Infinies.Combat
{
    [JsonDerivedType(typeof(DrawCardEvent))]
    [JsonDerivedType(typeof(EndMatchEvent))]
    [JsonDerivedType(typeof(GainManaEvent))]
    [JsonDerivedType(typeof(PlayerEndTurnEvent))]
    [JsonDerivedType(typeof(PlayerStartTurnEvent))]
    [JsonDerivedType(typeof(StartMatchEvent))]
    [JsonDerivedType(typeof(SurrenderEvent))]
    public abstract class MatchEvent
    {
        public abstract string EventType { get; }

        public MatchEvent()
        {
        }

        public List<MatchEvent>? Events { get; set; }
    }
}
