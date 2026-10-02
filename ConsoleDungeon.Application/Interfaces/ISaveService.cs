using ConsoleDungeon.Domain.Models;

namespace ConsoleDungeon.Application.Interfaces
{
    public interface ISaveService
    {
        void SaveGame(Player player);
        bool LoadGame();
    }
}