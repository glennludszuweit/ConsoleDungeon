namespace ConsoleDungeon.Game
{
    public static class Introduction
    {
        public static void IntroMessage(string name)
        {
            // --- Screen 1: The Gates ---
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          THE GATES OF BLACKWOOD          ");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("The heavy iron portcullis slams shut behind you, echoing through");
            Console.WriteLine("the damp, torch-lit stone corridors. The air smells of old dust");
            Console.WriteLine("and forgotten blood.");
            Console.WriteLine();
            Console.WriteLine($"You clutch your weapon, {name}, knowing there is only");
            Console.WriteLine("one way out: deeper into the dark.");
            Console.WriteLine();
            Console.WriteLine("Press any key continue...");
            Console.ReadKey();

            // --- Screen 2: Deeper into the Dark ---
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("            THE FIRST DESCENT             ");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("You descend a cracked spiral staircase, your boots crunching");
            Console.WriteLine("on loose gravel and shattered bone. Far below, a faint green glow");
            Console.WriteLine("flickers against the walls, accompanied by the sound of scuttling claws.");
            Console.WriteLine();
            Console.WriteLine("Whatever lives down here knows you've arrived.");
            Console.WriteLine();
            Console.WriteLine("Press any key to draw your weapon and begin...");
            Console.ReadKey();
        }

        public static void FirstEncounter()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          AMBUSH IN THE SHADOWS           ");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("You step off the staircase into a damp cavern. Suddenly, a rusted");
            Console.WriteLine("dagger clatters against the stones ahead of you. Out from the");
            Console.WriteLine("shadows lunges a **Wretched Goblin**, its eyes glowing yellow with");
            Console.WriteLine("malice and hunger!");
            Console.WriteLine();
            Console.WriteLine($"\"Fresh meat!\" screeches the creature, baring its jagged teeth.");
            Console.WriteLine();
            Console.WriteLine("Combat is about to begin...");
            Console.WriteLine("Press any key to face your first foe...");
            Console.ReadKey();
        }
    }
}