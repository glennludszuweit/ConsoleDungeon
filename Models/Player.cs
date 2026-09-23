namespace ConsoleDungeon.Models
{
    public class Player
    {
        public string Name { get; set; } = "Adventurer";
        public int Coins { get; set; } = 0;
        public int Health { get; set; } = 10;
        public int Damage { get; set; } = 1;
        public int WeaponValue { get; set; } = 1;
        public int ArmorValue { get; set; } = 0;
        public int Potion { get; set; } = 5;
    }
}