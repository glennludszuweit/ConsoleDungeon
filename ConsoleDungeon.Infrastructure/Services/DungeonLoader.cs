using System.Text.Json;
using ConsoleDungeon.Application.Interfaces;
using ConsoleDungeon.Domain.Models;

namespace ConsoleDungeon.Infrastructure.Services
{
    public class DungeonLoader : IDungeonLoader
    {
        private List<Enemy> _allEnemies = [];
        private List<Room> _allRooms = [];

        public DungeonLoader()
        {
            LoadData();
        }

        public async void LoadData()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                if (File.Exists("Data/enemies.json"))
                {
                    string json = File.ReadAllText("Data/enemies.json");
                    _allEnemies = JsonSerializer.Deserialize<List<Enemy>>(json, options) ?? [];
                }

                if (File.Exists("Data/rooms.json"))
                {
                    string json = File.ReadAllText("Data/rooms.json");
                    _allRooms = JsonSerializer.Deserialize<List<Room>>(json, options) ?? [];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON files: {ex.Message}");
            }
        }

        public List<Enemy> GetEnemiesForRoom(int roomNumber)
        {
            return [.. _allEnemies.Where(e => e.RoomLevel == roomNumber)];
        }

        public Room? GetRoomData(int roomNumber)
        {
            return _allRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }
    }
}