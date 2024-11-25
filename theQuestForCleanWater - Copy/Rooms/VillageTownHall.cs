using System.Collections.Generic;
namespace TextBasedGame.Rooms;


public class VillageTownHall : Room
{



    public VillageTownHall()
    : base("Village Town Hall", "A bustling place where the mayor discusses village issues. \n The townspeople seem concerned about their water problems.")
    {
        RoomNPC = new NPC(
                   "Mayor Anwar",
                   "You seem eager to help. If you fix some of our water problems, I'll give you the village map to aid your journey.",
                   "Keep up the good work! This village depends on people like you."
               );
    }

    public override void TalkToNPC(Game game)
    {
        if (!game.hasFinalItems)
        {

            if (LeakingPipelines.HasFixedPipelines)
            {
                if (!game.hasMap && !RiverUpstream.FishRodBroken)

                {
                    Text.PrintSeparator();
                    Text.PrintWrappedText("Mayor Anwar: I see you’ve fixed the pipelines. Well done, Alex! You’ve earned this.\"", "Mayor Anwar", ConsoleColor.Yellow);
                    Text.PrintWrappedText("You recive the village map. Use the command 'show map' to use it.", "village map", ConsoleColor.DarkMagenta);
                    game.Inventory.Add(new Item("Village Map", ConsoleColor.DarkMagenta, "A map showing all key locations in Aquara.")); // Add map to inventory
                    game.hasMap = true; // Mark that the player has received the map
                    Text.PrintSeparator();

                    if (RiverUpstream.FishRodBroken)
                    {
                        Text.PrintSeparator();
                        Console.WriteLine("Mayor Anwar: It looks like you broke your previous fishing rod, here you go a new one, please be careful this time.", "Mayor Anwar", ConsoleColor.Yellow);
                        RiverUpstream.FishRodBroken = false;

                    }

                }
                else if (!RiverUpstream.FishRodBroken)
                {
                    Text.PrintSeparator();
                    Text.PrintWrappedText("Mayor Anwar: I need to be sure you’re not just wasting our time.\"", "Mayor Anwar", ConsoleColor.Yellow);
                    Text.PrintWrappedText("I want you to gather a few essential items that will prove your dedication to our cause.");
                    Text.PrintWrappedText("These will also help us tackle our water crisis");
                    Text.PrintSeparator();
                    Text.PrintWrappedText("You must clean the river upstream and catch a fish to prove it's clean.\"", "catch a fish", ConsoleColor.Red);
                    Console.WriteLine();
                    Text.PrintWrappedText("Fix water harvesting system and bring me a jar of rainwater.", "jar of rainwater", ConsoleColor.Red);
                    Console.WriteLine();
                    Text.PrintWrappedText("Purify the water in the storage tanks and bring me the Purified Water Crystal to ensure it's safe.\"", "Purified Water Crystal", ConsoleColor.Red);
                    Console.WriteLine();
                    Text.PrintWrappedText("Gather water from the dry well using a Rusty Bucket.", "Rusty Bucket", ConsoleColor.Red);
                    Console.WriteLine();
                    Text.PrintWrappedText("Come back to me with the Fish, the Jar of Fresh Rainwater, the Purified Water Crystal, and the Rusty Bucket. Only then can we move forward.\"");
                }
                else
                {
                    Text.PrintSeparator();
                    Text.PrintWrappedText("Mayor Anwar: It looks like you broke your previous fishing rod, here you go a new one, please be careful this time.", "Mayor Anwar", ConsoleColor.Yellow);
                    RiverUpstream.FishRodBroken = false;

                }

            }
            else if (!RiverUpstream.FishRodBroken)
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Mayor Anwar: I heard you're trying to help with our water issues. But unless you fix those pipelines I can’t trust you with the village map. You can find the pipelines to the south of the town hall. Engineer Tomas will be there to help you. But you can choose any direction to start your journey.\"", "Mayor Anwar", ConsoleColor.Yellow);

            }
            else
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Mayor Anwar: It looks like you broke your previous fishing rod, here you go a new one, please be careful this time.", "Mayor Anwar", ConsoleColor.Yellow);
                RiverUpstream.FishRodBroken = false;


            }
        }
        else
        {
            if (game.Score >= 165 && game.Score < 200)
            {
                game.DisplayBasicEnding();
            }
            else if (game.Score >= 200 && game.Score < 240)
            {
                game.DisplayStandardEnding();
            }
            else if (game.Score >= 240)
            {
                game.DisplayPerfectEnding();
            }


        }
    }

}
