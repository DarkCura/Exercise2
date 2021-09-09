using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayingCards.Constants;
using PlayingCards.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlayingCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Random random = new Random();
            return View(new IndexViewModel() { RandomCards = Enumerable.Range(0, 52).OrderBy((a) => random.Next(52)).Take(5).ToArray() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EvaluateHand([FromBody] PokerCardRequest[] pokerHandRequest)
        {
            Dictionary<int, int> valueCount = pokerHandRequest.Select(x => x.Value).Distinct().ToDictionary(x => x, x => pokerHandRequest.Where(y => y.Value == x).Count());
            Dictionary<string, int> suitCount = pokerHandRequest.Select(x => x.Suit).Distinct().ToDictionary(x => x, x => pokerHandRequest.Where(y => y.Suit == x).Count());

            // Straight Flush
            if (suitCount.Keys.Count() == 1 && valueCount.Values.OrderBy(x => x).SequenceEqual(Enumerable.Range(valueCount.Values.Min(), valueCount.Count())))
                return Ok(new PokerHandResponse { Status = 200, HandType = "Straight Flush" });

            // Four of a Kind
            if (valueCount.Values.Any(x => x == 4))
                return Ok(new PokerHandResponse { Status = 200, HandType = "Four of a Kind" });

            // Full House | Three of a Kind
            if (valueCount.Values.Any(x => x == 3))
            {
                // Full House
                if (valueCount.Values.Any(x => x == 2))
                    return Ok(new PokerHandResponse { Status = 200, HandType = "Full House" });
                // Three of a Kind
                return Ok(new PokerHandResponse { Status = 200, HandType = "Three of a Kind" });
            }

            // Flush
            if (suitCount.Keys.Count() == 1)
                return Ok(new PokerHandResponse { Status = 200, HandType = "Flush" });

            // Straight
            if (valueCount.Values.OrderBy(x => x).SequenceEqual(Enumerable.Range(valueCount.Values.Min(), valueCount.Count())))
                return Ok(new PokerHandResponse { Status = 200, HandType = "Straight" });

            // Two Pair
            if (valueCount.Values.Where(x => x == 2).Count() == 2)
                return Ok(new PokerHandResponse { Status = 200, HandType = "Two Pair" });
            
            // Pair
            if (valueCount.Values.Any(x => x == 2))
                return Ok(new PokerHandResponse { Status = 200, HandType = $"Pair of { Cards.RANKS[Array.IndexOf<int>(Cards.VALUES, valueCount.FirstOrDefault(x => x.Value == 2).Key)]}s" });

            // High Card
            return Ok(new PokerHandResponse { Status = 200, HandType = "High Card" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
