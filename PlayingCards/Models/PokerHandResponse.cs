using System.Text.Json.Serialization;

namespace PlayingCards.Models
{
    public class PokerHandResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("type")]
        public string HandType { get; set; }
    }
}
