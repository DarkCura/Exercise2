using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayingCards.Constants;
using PlayingCards.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            // Straight flush (same suit in sequence)
            // Four of a kind
            // Full house (pair and three of a kind)
            // Flush (same suit)
            // Straight (numerically in order)
            // Three of a kind
            // Two pair
            // Pair
            // High card
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EvaluateHand([FromBody] PokerCardRequest[] pokerHandRequest)
        {
            Dictionary<int, int> valueCount = pokerHandRequest.Select(x => x.Value).Distinct().ToDictionary(x => x, x => pokerHandRequest.Where(y => y.Value == x).Count());
            Dictionary<string, int> suitCount = pokerHandRequest.Select(x => x.Suit).Distinct().ToDictionary(x => x, x => pokerHandRequest.Where(y => y.Suit == x).Count());

            // Straight Flush
            if (suitCount.Keys.Count() == 1 && valueCount.Values.OrderBy(x => x).SequenceEqual(Enumerable.Range(valueCount.Values.Min(), valueCount.Count())))
                return Ok(new { status = 200, type = "Straight Flush" });

            // Four of a Kind
            if (valueCount.Values.Any(x => x == 4))
                return Ok(new { status = 200, type = "Four of a Kind" });

            // Full House | Three of a Kind
            if (valueCount.Values.Any(x => x == 3))
            {
                // Full House
                if (valueCount.Values.Any(x => x == 2))
                    return Ok(new { status = 200, type = "Full House" });
                // Three of a Kind
                return Ok(new { status = 200, type = "Three of a Kind" });
            }

            // Flush
            if (suitCount.Keys.Count() == 1)
                return Ok(new { status = 200, type = "Flush" });

            // Straight
            if (valueCount.Values.OrderBy(x => x).SequenceEqual(Enumerable.Range(valueCount.Values.Min(), valueCount.Count())))
                return Ok(new { status = 200, type = "Straight" });

            // Two Pair
            if (valueCount.Values.Where(x => x == 2).Count() == 2)
                return Ok(new { status = 200, type = "Two Pair" });
            
            // Pair
            if (valueCount.Values.Any(x => x == 2))
                return Ok(new { status = 200, type = $"Pair of { Cards.RANKS[Array.IndexOf<int>(Cards.VALUES, valueCount.FirstOrDefault(x => x.Value == 2).Key)]}s" });

            // High Card
            return Ok(new { status = 200, type = "High Card" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
