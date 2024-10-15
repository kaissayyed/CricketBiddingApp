namespace CricketBiddingApp.Api.Models
{
    public class AuctionRule
    {
        public int Id { get; set; }
        public int PlayerLimit { get; set; } // Maximum players a team can buy
        public decimal MaxBudget { get; set; } // Maximum amount a team can spend
    }

}
