using System.Text.Json.Serialization;

namespace CricketBiddingApp.Api.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Team> Teams { get; set; } = new List<Team>();
        public decimal TotalBudget { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

}
