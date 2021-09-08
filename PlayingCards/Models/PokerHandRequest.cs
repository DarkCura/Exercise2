using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PlayingCards.Models
{
    public class PokerCardRequest
    {
        [JsonPropertyName("suit")]
        public string Suit { get; set; }
        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
