using ConsoleDungeon.Models;
using ConsoleDungeon.Services;

namespace ConsoleDungeon.Game
{
    public class GameEngine
    {
        private readonly Player _player;
        private readonly Encounters _encounters;
        private readonly DungeonLoader _dungeonLoader;
        private readonly SaveService _saveService;

        public GameEngine(Player player, Encounters encounters, DungeonLoader dungeonLoader, SaveService saveService)
        {
            _player = player;
            _encounters = encounters;
            _dungeonLoader = dungeonLoader;
            _saveService = saveService;
        }

        public void Run()
        {
            LoadOrNewGame();
            RunIntroSequence();

            // Loop through all 5 rooms defined in rooms.json
            for (int roomNum = 1; roomNum <= 5; roomNum++)
            {
                var roomInfo = _dungeonLoader.GetRoomData(roomNum);
                if (roomInfo == null) break;

                bool roomCleared = PlayRoom(roomInfo);

                if (!roomCleared || _player.Health <= 0)
                {
                    DisplayGameOver();
                    return;
                }

                // Show transition story if not the final room
                if (roomNum < 5 && !string.IsNullOrEmpty(roomInfo.TransitionStory))
                {
                    DisplayTransition(roomInfo.TransitionStory);
                }
            }

            DisplayGameVictory();
        }

        private void LoadOrNewGame()
        {
            if (File.Exists("Data/save-game.json"))
            {
                Console.Write("Saved game detected! Would you like to load your previous adventure (y/N)? ");
                var loadGame = Console.ReadLine() ?? "N";

                if (loadGame.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    _saveService.LoadGame();
                }
                else
                {
                    SetupPlayer();
                }
            }
            else
            {
                SetupPlayer();
            }
        }

        private void SetupPlayer()
        {
            Console.Clear();
            Console.WriteLine("Console Dungeon!");
            Console.WriteLine("----------------");
            Console.Write("Enter your Name: ");
            _player.Name = Console.ReadLine() ?? "Adventurer";
        }

        private void RunIntroSequence()
        {
            Introduction.IntroMessage(_player.Name);
            Introduction.FirstEncounter();
        }

        private bool PlayRoom(Room room)
        {
            List<Enemy> roomEnemies = _dungeonLoader.GetEnemiesForRoom(room.RoomNumber);

            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine($"       ROOM {room.RoomNumber}: {room.Title.ToUpper()}       ");
            Console.WriteLine("==========================================");
            Console.WriteLine(room.Theme);
            Console.WriteLine();
            Console.WriteLine($"There are {roomEnemies.Count} foul creatures waiting in the dark here...");
            Console.WriteLine("Press any key to face them!");
            Console.ReadKey();

            // Use a while loop instead of foreach so we can control index tracking if we run away
            int enemyIndex = 0;
            while (enemyIndex < roomEnemies.Count)
            {
                if (_player.Health <= 0) return false;

                var enemy = roomEnemies[enemyIndex];
                Enemy currentFoe = new Enemy
                {
                    Name = enemy.Name,
                    Power = enemy.Power,
                    Health = enemy.Health,
                    CoinReward = enemy.CoinReward
                };

                bool won = _encounters.Combat(false, currentFoe.Name, currentFoe.Power, currentFoe.Health);

                if (_player.Health <= 0) return false;

                if (!won)
                {
                    // --- RETREAT TO THE SAFE HALLWAY ---
                    SafeHallway();
                    // Loop stays on the same enemyIndex, so they re-fight the same foe!
                    continue;
                }

                _player.Coins += currentFoe.CoinReward;
                Console.WriteLine($"\nYou searched the fallen {currentFoe.Name} and collected {currentFoe.CoinReward} coins!");
                Console.WriteLine("Press any key to steel yourself for the next threat...");
                Console.ReadKey();

                enemyIndex++; // Move to the next enemy in the room
            }

            return true;
        }

        private void SafeHallway()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("           THE SAFE ALCOVE                ");
                Console.WriteLine("==========================================");
                Console.WriteLine("You catch your breath in a quiet, shadowy nook.");
                Console.WriteLine("A wandering merchant's stall and a warm hearth await you.");
                Console.WriteLine();
                Console.WriteLine($"Hero: {_player.Name}");
                Console.WriteLine($"Health: {_player.Health} | Armor: {_player.ArmorValue} | Damage: {_player.Damage}");
                Console.WriteLine($"Coins:  {_player.Coins}  | Potions: {_player.Potion}");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine(" (1) Rest and Heal      (Cost: 15 Coins -> +25 HP)");
                Console.WriteLine(" (2) Buy a Potion       (Cost: 10 Coins -> +1 Potion)");
                Console.WriteLine(" (3) Sharpen Weapon     (Cost: 25 Coins -> +2 Damage)");
                Console.WriteLine(" (4) Reinforce Armor    (Cost: 20 Coins -> +5 Armor)");
                Console.WriteLine(" (5) Return to Dungeon Fight");
                Console.WriteLine(" (6) Save Game");
                Console.WriteLine("==========================================");
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (input == "1")
                {
                    if (_player.Coins >= 15)
                    {
                        _player.Coins -= 15;
                        _player.Health += 25;
                        Console.WriteLine("\nYou tend to your wounds by the warm hearth (+25 Health).");
                    }
                    else
                    {
                        Console.WriteLine("\nYou don't have enough coins! (Needs 15)");
                    }
                    Console.ReadKey();
                }
                else if (input == "2")
                {
                    if (_player.Coins >= 10)
                    {
                        _player.Coins -= 10;
                        _player.Potion++;
                        Console.WriteLine("\nYou purchase a bubbling glass vial of red liquid (+1 Potion).");
                    }
                    else
                    {
                        Console.WriteLine("\nYou don't have enough coins to buy a potion! (Needs 10)");
                    }
                    Console.ReadKey();
                }
                else if (input == "3")
                {
                    if (_player.Coins >= 25)
                    {
                        _player.Coins -= 25;
                        _player.Damage += 2;
                        Console.WriteLine($"\nYou grind your weapon against the whetstone. It gleams with lethal intent! (Damage is now {_player.Damage})");
                    }
                    else
                    {
                        Console.WriteLine("\nYou don't have enough coins to upgrade your weapon! (Needs 25)");
                    }
                    Console.ReadKey();
                }
                else if (input == "4")
                {
                    if (_player.Coins >= 20)
                    {
                        _player.Coins -= 20;
                        _player.ArmorValue += 5;
                        Console.WriteLine($"\nYou rivet thick plates onto your gear. You feel much safer! (Armor is now {_player.ArmorValue})");
                    }
                    else
                    {
                        Console.WriteLine("\nYou don't have enough coins to reinforce your armor! (Needs 20)");
                    }
                    Console.ReadKey();
                }
                else if (input == "5")
                {
                    Console.WriteLine("\nYou grip your weapon tightly and head back toward the danger...");
                    Console.ReadKey();
                    break; // Exit the safe hallway loop and go back to combat
                }
                else if (input == "6")
                {
                    _saveService.SaveGame(_player);
                    Console.WriteLine("\nProgress successfuly saved!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Choose 1 through 5.");
                    Console.ReadKey();
                }
            }
        }

        private void DisplayTransition(string storyText)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("            DEEPER INTO THE DARK          ");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine(storyText);
            Console.WriteLine();
            Console.WriteLine("Press any key to push onward...");
            Console.ReadKey();
        }

        private void DisplayGameOver()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine("                  YOU HAVE DIED                 ");
            Console.WriteLine("================================================");
            Console.WriteLine($"Your journey ends here in the dark, {_player.Name}.");
            Console.ReadKey();
        }

        private void DisplayGameVictory()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine("                 VICTORY!                       ");
            Console.WriteLine("================================================");
            Console.WriteLine($"The Dungeon Lord falls! Silence blankets the corridors.");
            Console.WriteLine($"You emerge into the sunlight carrying {_player.Coins} coins, a true hero, {_player.Name}!");
            Console.ReadKey();
        }
    }
}