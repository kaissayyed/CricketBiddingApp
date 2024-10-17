using System.Text.Json.Serialization;

namespace CricketBiddingApp.Api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // Diamond, Gold, Silver, Bronze
        public bool IsSold { get; set; } // Flag to check if the player is sold
        public decimal? SoldPrice { get; set; }// The price at which the player was sold (0 if not sold)
    }
}
