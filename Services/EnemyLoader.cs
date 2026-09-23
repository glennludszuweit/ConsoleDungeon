using System.Text.Json;
using ConsoleDungeon.Models;

namespace ConsoleDungeon.Services
{
    public class EnemyLoader
    {
        private List<Enemy> _enemies = [];

        public EnemyLoader()
        {
            LoadEnemies();
        }

        private void LoadEnemies()
        {
            try
            {
                string filePath = "enemies.json";
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    _enemies = JsonSerializer.Deserialize<List<Enemy>>(jsonString) ?? [];
                }
                else
                {
                    // Fallback if file isn't found
                    _enemies.Add(new Enemy { Name = "Wretched Goblin", Power = 2, Health = 8 });
                }
            }
            catch (Exception)
            {
                // Fallback safe default
                _enemies.Add(new Enemy { Name = "Wretched Goblin", Power = 2, Health = 8 });
            }
        }

        public Enemy GetRandomEnemy()
        {
            if (_enemies.Count == 0) return new Enemy { Name = "Goblin", Power = 2, Health = 8 };

            Random rand = new();
            int index = rand.Next(_enemies.Count);

            // Return a fresh clone object so modifying its health during combat doesn't break the master list
            var template = _enemies[index];
            return new Enemy
            {
                Name = template.Name,
                Power = template.Power,
                Health = template.Health
            };
        }
    }
}