using System.Text.Json.Serialization;

namespace CricketBiddingApp.Api.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } // To track if the auction is ongoing
        public  AuctionRule AuctionRule { get; set; } // Navigation property
    }

}
