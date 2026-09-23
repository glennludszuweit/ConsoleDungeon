namespace ConsoleDungeon.Models
{
    public class Enemy
    {
        public string Name { get; set; } = "Unknown Beast";
        public int Power { get; set; } = 1;
        public int Health { get; set; } = 10;
        public int CoinReward { get; set; } = 5;
        public int RoomLevel { get; set; } = 1;
    }
}