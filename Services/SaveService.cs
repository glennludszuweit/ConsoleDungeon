using System.Text.Json;
using ConsoleDungeon.Models;
using Microsoft.Extensions.FileProviders;

namespace ConsoleDungeon.Services
{
    public class SaveService
    {
        private readonly Player _player;

        public SaveService(Player player)
        {
            _player = player;
        }

        public void SaveGame(Player player)
        {
            var serializedPlayer = JsonSerializer.Serialize(player);
            File.WriteAllText("Data/save-game.json", serializedPlayer);
        }

        public bool LoadGame()
        {
            try
            {
                if (File.Exists("Data/save-game.json"))
                {
                    string json = File.ReadAllText("Data/save-game.json");
                    var loadedPlayer = JsonSerializer.Deserialize<Player>(json);

                    if (loadedPlayer != null)
                    {
                        _player.Name = loadedPlayer.Name;
                        _player.Coins = loadedPlayer.Coins;
                        _player.Health = loadedPlayer.Health;
                        _player.Damage = loadedPlayer.Damage;
                        _player.WeaponValue = loadedPlayer.WeaponValue;
                        _player.ArmorValue = loadedPlayer.ArmorValue;
                        _player.Potion = loadedPlayer.Potion;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON files: {ex.Message}");
            }

            return false;
        }
    }
}