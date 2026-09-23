using System.Text.Json;
using ConsoleDungeon.Models;

namespace ConsoleDungeon.Services
{
    public class DungeonLoader
    {
        private List<Enemy> _allEnemies = [];
        private List<RoomData> _allRooms = [];

        public DungeonLoader()
        {
            LoadData();
        }

        private void LoadData()
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
                    _allRooms = JsonSerializer.Deserialize<List<RoomData>>(json, options) ?? [];
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

        public RoomData? GetRoomData(int roomNumber)
        {
            return _allRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }
    }
}