namespace CricketBiddingApp.Api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public decimal BudgetSpent { get; set; } // Total budget spent on players
        public int PlayerCount { get; set; } // Count of players bought
        public ICollection<Player> Players { get; set; }
    }
}
