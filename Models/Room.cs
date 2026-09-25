namespace ConsoleDungeon.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Title { get; set; } = "";
        public string Theme { get; set; } = "";
        public string TransitionStory { get; set; } = "";
    }
}