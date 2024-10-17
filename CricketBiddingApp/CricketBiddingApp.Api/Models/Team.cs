namespace CricketBiddingApp.Api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public int MaxPlayers { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
