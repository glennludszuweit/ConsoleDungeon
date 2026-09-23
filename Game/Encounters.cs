using ConsoleDungeon.Models;

namespace ConsoleDungeon.Game
{
    public class Encounters
    {
        private readonly Player _player;
        private readonly Random _random = new();

        public Encounters(Player player)
        {
            _player = player;
        }

        public bool Combat(bool random, string name, int power, int health)
        {
            string n = name;
            int p = power;
            int h = health;

            while (h > 0 && _player.Health > 0)
            {
                Console.Clear();
                Console.WriteLine($"Enemy: {n} | Health: {h} | Power: {p}");
                Console.WriteLine($"Hero:  {_player.Name} | Health: {_player.Health} | Armor: {_player.ArmorValue}");
                Console.WriteLine();

                Console.WriteLine("================================================");
                Console.WriteLine("| (A) Attack - (D) Defend - (R) Run - (H) Heal |");
                Console.WriteLine("================================================");
                Console.WriteLine($"Potions: {_player.Potion} | Coins: {_player.Coins}");

                Console.Write("> ");
                string? input = Console.ReadLine()?.ToUpper();

                bool playerActed = true;
                bool isDefending = false; // Track if player raised their guard

                switch (input)
                {
                    case "A":
                        int playerDamage = _player.Damage + _player.WeaponValue + _random.Next(0, 3);
                        h -= playerDamage;
                        Console.WriteLine();
                        Console.WriteLine($"You strike the {n}, dealing {playerDamage} damage!");
                        break;

                    case "D":
                        isDefending = true; // Player is defending this turn!
                        Console.WriteLine();
                        Console.WriteLine("You raise your guard, bracing yourself for the incoming blow...");
                        break;

                    case "R":
                        Console.WriteLine();
                        Console.WriteLine("You successfully escape back up the corridor!");
                        Console.ReadKey();
                        return false;

                    case "H":
                        if (_player.Potion > 0)
                        {
                            _player.Potion--;
                            int healAmount = 5;
                            _player.Health += healAmount;
                            Console.WriteLine();
                            Console.WriteLine($"You drink a potion and recover {healAmount} health!");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("You search your pockets... but you are out of potions!");
                            playerActed = false;
                        }
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid choice! Choose (A), (D), (R), or (H).");
                        playerActed = false;
                        break;
                }

                // Enemy retaliation phase
                if (h > 0 && playerActed)
                {
                    int enemyDamage = _random.Next(p, p + 3);
                    Console.WriteLine();

                    // Apply Defend reduction (10% off enemy damage if defending)
                    if (isDefending)
                    {
                        // Reduce damage by 10% (keeping it as an int, rounded or casted down)
                        int reducedDamage = (int)(enemyDamage * 0.90);
                        // Ensure at least 1 damage goes through if enemy power is > 0
                        if (reducedDamage < 1 && enemyDamage > 0) reducedDamage = 1;

                        Console.WriteLine($"Your defense softens the blow! Enemy damage reduced from {enemyDamage} to {reducedDamage}.");
                        enemyDamage = reducedDamage;
                    }

                    // --- ARMOR ABSORPTION LOGIC ---
                    if (_player.ArmorValue > 0)
                    {
                        if (_player.ArmorValue >= enemyDamage)
                        {
                            _player.ArmorValue -= enemyDamage;
                            Console.WriteLine($"Your armor absorbs the full attack! (Armor left: {_player.ArmorValue})");
                        }
                        else
                        {
                            int leftoverDamage = enemyDamage - _player.ArmorValue;
                            Console.WriteLine($"Your armor absorbs {_player.ArmorValue} damage before breaking! The remaining {leftoverDamage} damage strikes your flesh.");
                            _player.ArmorValue = 0;
                            _player.Health -= leftoverDamage;
                        }
                    }
                    else
                    {
                        _player.Health -= enemyDamage;
                        Console.WriteLine($"The {n} strikes you directly, dealing {enemyDamage} damage to your health!");
                    }
                }
                else if (h <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"With a final gasp, the {n} collapses to the stone floor!");

                    int coinReward = _random.Next(2, 6);
                    _player.Coins += coinReward;
                    Console.WriteLine($"You search the remains and find {coinReward} coins.");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return true;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            if (_player.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("================================================");
                Console.WriteLine("                  YOU HAVE DIED                 ");
                Console.WriteLine("================================================");
                Console.WriteLine($"Your journey ends here in the dark, {_player.Name}.");
                Console.ReadKey();
            }

            return false;
        }
    }
}