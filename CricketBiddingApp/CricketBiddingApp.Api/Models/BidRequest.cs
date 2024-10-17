namespace CricketBiddingApp.Api.Models
{
    public class BidRequest
    {
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public decimal BidAmount { get; set; }
    }
}
