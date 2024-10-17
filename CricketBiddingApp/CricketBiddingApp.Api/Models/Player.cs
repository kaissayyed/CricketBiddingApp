using System.Text.Json.Serialization;

namespace CricketBiddingApp.Api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // Diamond, Gold, Silver, Bronze
        public bool IsSold { get; set; } // Flag to check if the player is sold
        public decimal CurrentBid { get; set; }
        public int WinningTeamId { get; set; }
    }
}
