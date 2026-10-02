using ConsoleDungeon.Domain.Models;

namespace ConsoleDungeon.Application.Interfaces
{
    public interface IDungeonLoader
    {
        void LoadData();
        List<Enemy> GetEnemiesForRoom(int roomNumber);
        Room? GetRoomData(int roomNumber);
    }
}