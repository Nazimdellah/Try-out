using System.Text.Json.Serialization;

namespace Super_Cartes_Infinies.Models.Dtos
{
    public class JoiningMatchData
    {
        public Match Match { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public bool IsStarted { get; set; }
        [JsonIgnore]
        public string OtherPlayerConnectionId { get; set; }
    }
}
