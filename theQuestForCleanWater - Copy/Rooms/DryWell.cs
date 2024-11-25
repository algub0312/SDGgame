namespace TextBasedGame.Rooms;

public class DryWell : Room
{

    public static bool hasRepairedWell = false;
    public static bool hasRustyBucket { get; set; } = false;
    public static bool decision = false;

    public DryWell()
    : base("Dry Well", "An abandoned well at the edge of the village. \nThere's just a trickle of murky water at the bottom, and it seems forgotten by most villagers.")
    {
        RoomNPC = new NPC(
            "Old Sara",
            "Ah, young one. This well used to bring life to our village. \n Now it's almost dry... but it can be saved if we find a way to purify its water.",
            "The well's hope lies in your hands, young one. Keep going."
        );

    }
    public override void UseItem(Game game, string itemName)
    {
        string normalizedItemName = itemName.Trim().ToLower();
        Item foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);
        if ((game.CurrentRoom.Name == "Dry Well") && foundItem.Name.ToLower() == "water testing kit")
        {
            if (RiverUpstream.hasCleanedRiver && LeakingPipelines.HasFixedPipelines && decision)
            {
                if (!hasRepairedWell)
                {
                    Console.Clear();
                    Text.PrintWrappedText("You use the Water Testing Kit to test the water. The results show that the water is now safe for use!", "Water Testing Kit", ConsoleColor.DarkMagenta);
                    game.IncreaseScore(50);
                    hasRepairedWell = true;
                    Item rustyBucket = new Item("Rusty Bucket", ConsoleColor.DarkMagenta, "An old bucket, maybe it will be useful.");
                    game.ReceiveItem(rustyBucket);
                    hasRustyBucket = true;
                    Text.PrintSeparator();
                    game.CheckFinalItems(game);
                }

            }
            if (RiverUpstream.hasCleanedRiver && LeakingPipelines.HasFixedPipelines && !decision)
            {
                if (!hasRepairedWell)
                {
                    Console.Clear();
                    Text.PrintWrappedText("You use the Water Testing Kit to test the water. The results show that the water is now safe for use!", "Water Testing Kit", ConsoleColor.DarkMagenta);
                    game.IncreaseScore(30);
                    hasRepairedWell = true;
                    Item rustyBucket = new Item("Rusty Bucket", ConsoleColor.DarkMagenta, "An old bucket, maybe it will be useful.");
                    game.ReceiveItem(rustyBucket);
                    hasRustyBucket = true;
                    Text.PrintSeparator();

                    game.CheckFinalItems(game);
                }

            }
            else if (!RiverUpstream.hasCleanedRiver && LeakingPipelines.HasFixedPipelines)
            {
                Console.Clear();
                Text.PrintWrappedText("You use the Water Testing Kit, but the results show the water is still contaminated.", "Water Testing Kit", ConsoleColor.DarkMagenta);
            }
            else if (!RiverUpstream.hasCleanedRiver && !LeakingPipelines.HasFixedPipelines)
            {
                Console.Clear();
                Text.PrintWrappedText("Try fixing the pipelines first, you cannot reach the water.", "fixing the pipelines", ConsoleColor.DarkRed);
            }
        }
        if (game.CurrentRoom.Name == "Dry Well" && foundItem.Name.ToLower() == "water purification tablets")
        {
            if (!hasRepairedWell && !RiverUpstream.hasCleanedRiver)
            {
                Console.Clear();
                Text.PrintWrappedText("You used the Water Purification Tablets to purify the well. The well is fine for now but this won't last for long", "Water Purification Tablets", ConsoleColor.DarkMagenta);
                game.IncreaseScore(10);
                hasRepairedWell = true;
                Item rustyBucket = new Item("Rusty Bucket", ConsoleColor.DarkMagenta, "An old bucket, maybe it will be useful.");
                game.ReceiveItem(rustyBucket);
                hasRustyBucket = true;
                Text.PrintSeparator();
                game.CheckFinalItems(game);

            }

        }
    }
    public override void TalkToNPC(Game game)
    {
        if (!LeakingPipelines.HasFixedPipelines && !RiverUpstream.hasCleanedRiver)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: Ah, young one. This well used to bring life to our village. Now it's almost dry... but it can be saved if we find a way to purify its water.\"", "Old Sara", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. \"I’ll do everything I can to bring water back. Can you guide me?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"What caused the well to dry up?\"", "2", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("3. \"I don't think anything can be done for the well now.\"", "3", ConsoleColor.DarkGreen);

            bool validResponse = false;
            while (!validResponse)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Old Sara: That’s the spirit! I’ll share what I know. Restoring this well won’t be easy, but your efforts will ensure a brighter future for the village. Start by examining the water table levels and clearing the blockages in the old pipelines.", "Old Sara", ConsoleColor.Yellow);
                        NPC.ProvideHints(game); // Start hints dialogue
                        validResponse = true;
                        break;

                    case "2":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Old Sara: A great question, young one. The well dried up because of years of neglect and overuse. The pipelines are clogged, and the river is polluted. Climate changes and deforestation have also reduced the natural water levels.", "Old Sara", ConsoleColor.Yellow);
                        Text.PrintWrappedText("We must address these issues, or the same fate could befall other wells in the region.");
                        NPC.ProvideHints(game); // Start hints dialogue
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;

                    case "3":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Old Sara: Never give up hope, young one. The well may look beyond repair, but with enough care and dedication, even this symbol of despair can be brought back to life.", "Old Sara", ConsoleColor.Yellow);
                        Text.PrintWrappedText("Remember, every drop of water counts. If we turn away now, the village may never recover.");
                        validResponse = true;
                        break;

                    default:
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Old Sara: Please choose a valid option, young one. The well’s fate depends on your decision.");
                        break;
                }
            }
        }
        else if (LeakingPipelines.HasFixedPipelines && !RiverUpstream.hasCleanedRiver)
        {
            Text.PrintSeparator();
            // If pipelines are fixed, describe the well and offer new options
            Text.PrintWrappedText("Old Sara: The well is looking much better now that the pipelines have been fixed! We have water in the well!\"", "Old Sara", ConsoleColor.Yellow);
            Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Use the command use water testing kit.", "water testing kit", ConsoleColor.DarkMagenta);

            // New dialogue options after fixing the pipelines
            Text.PrintWrappedText("1. \"Can I use the Water Testing Kit to check the water quality?\"", "Water Testing Kit", ConsoleColor.DarkMagenta);
            Text.PrintWrappedText("2. \"Can you tell me more about the importance of the well for our village?\"");

            bool validResponse = false;
            while (!validResponse)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (!game.hasWaterTestingKit)
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("You need to obtain the Water Testing Kit first to analyze the water quality.", "Water Testing Kit", ConsoleColor.DarkMagenta);
                            Text.PrintWrappedText("I've heard that it can be optained from the Community Health Center.", "Community Health Center", ConsoleColor.Blue);
                        }
                        else
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("You use the Water Testing Kit to analyze the well water.", "Water Testing Kit", ConsoleColor.DarkMagenta);
                            NPC.OldSaraDiscussPurificationChoice(game);
                        }
                        validResponse = true;
                        break;

                    case "2":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Old Sara: Ah, the well is more than just a source of water for our village—it is the heart of our community. It has sustained us for generations, providing clean water for drinking, cooking, and farming.", "Old Sara", ConsoleColor.Yellow);
                        Text.PrintWrappedText("But its importance goes beyond survival. The well represents our resilience and connection to nature. Without it, our crops would fail, livestock would suffer, and life here would become unbearably hard.");
                        Text.PrintWrappedText("It also teaches us a valuable lesson: we must protect and conserve our resources, ensuring that future generations can thrive. Reviving this well is not just about water—it’s about preserving our way of life and our bond with the land.");
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;

                    default:
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Please choose a valid option (1 or 2).");
                        break;
                }
            }
        }
        else if (!LeakingPipelines.HasFixedPipelines && RiverUpstream.hasCleanedRiver)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: Young one, even if you have cleaned the river, the water still doesn't reach the well...", "Old Sara", ConsoleColor.Yellow);
            Text.PrintWrappedText("Try looking into fixing the pipelines in our town.");
        }
        else if (LeakingPipelines.HasFixedPipelines && RiverUpstream.hasCleanedRiver && !hasRepairedWell)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: Now after you repaired the pipes and cleaned the river, the well should be alright.", "Old Sara", ConsoleColor.Yellow);
            Text.PrintWrappedText("Try using the Water Testing Kit to see if the water is good now.", "Water Testing Kit", ConsoleColor.DarkMagenta);


        }
        if (Name == "Old Sara" && hasRepairedWell && hasRustyBucket)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: Thank you for all the help offered young one!", "Old Sara", ConsoleColor.Yellow);
        }
    }
}
