namespace CricketBiddingApp.Api.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public int AuctionRuleId { get; set; } // Reference to auction rules
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } // To track if the auction is ongoing

        public virtual AuctionRule AuctionRule { get; set; } // Navigation property
    }

}
