namespace TextBasedGame.Rooms;


public class LeakingPipelines : Room
{
    public static bool HasFixedPipelines = false;
    public bool haschosen2ndPipes = false;

    public LeakingPipelines()
    : base("Leaking Pipelines", "A network of old, rusted pipes leaking water. \nThe village's main water source flows through these pipes, but they're in dire need of repair.")
    {
        RoomNPC = new NPC(
                    "Engineer Tomas",
                    "These pipes are old and damaged. If we don't fix them soon, the entire village will lose its water supply.",
                    "Keep going. The pipes are our lifeline to clean water."
                );
    }
    public override void UseItem(Game game, string itemName)
    {
        string normalizedItemName = itemName.Trim().ToLower();

        if (normalizedItemName == "wrench" && !HasFixedPipelines)
        {
            if (!HasFixedPipelines)
            {
                var pipePuzzle = new ClearCloggedPipes();
                bool puzzleSolved = pipePuzzle.Start();
                if (puzzleSolved)
                {
                    game.IncreaseScore(20);  // Award points for fixing the pipelines
                    HasFixedPipelines = true; // Mark the pipelines as fixed
                    Text.PrintWithColor("Pipelines successfully fixed!", ConsoleColor.Green);
                    Text.PrintWithColor("(exits: north, south, east, west)", ConsoleColor.Green);

                    ((NPC)game.CurrentRoom.RoomNPC).CompleteQuest();
                }
                else
                {
                    Text.PrintWithColor("You were unable to fix the pipelines", ConsoleColor.Red);
                }
            }
            else
            {
                Text.PrintWithColor("You already fixed the pipelines.", ConsoleColor.Yellow);
            }
        }
    }
    public override void TalkToNPC(Game game)
    {
        if (!HasFixedPipelines)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Engineer Tomas: These pipes are old and damaged. If we don't fix them soon, the entire village will lose its water supply.\"", "Engineer Tomas", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. \"What tools do I need to fix the pipes?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"How bad is the damage?\"", "2", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("3. \"I don't think I can help with this.\"", "3", ConsoleColor.DarkGreen);

            bool validResponse = false;
            while (!validResponse)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("You'll need a wrench to tighten the connections.", "wrench", ConsoleColor.DarkMagenta);
                        Text.PrintWrappedText("I heard there's one in the Village Town Hall. Find it and come back here to help.", "Village Town Hall", ConsoleColor.Blue);
                        Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Once you return use the command use wrench.", "use wrench", ConsoleColor.DarkMagenta);

                        validResponse = true;
                        break;

                    case "2":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Engineer Tomas: I'm glad you asked. The damage is extensive—years of neglect have caused severe leaks. If we don't repair these pipes:", "Engineer Tomas", ConsoleColor.Yellow);
                        Text.PrintWrappedText("We'll lose most of our clean water to leaks, leaving less for drinking, farming, and sanitation.");
                        Text.PrintWrappedText("Contaminated water from the ground could seep into the pipes, causing illnesses like cholera and dysentery.");
                        Text.PrintWrappedText("Crops and livestock relying on water will suffer, which can lead to food shortages and economic collapse.");
                        Text.PrintWrappedText("This is why maintaining water infrastructure is so crucial—not just for us but for every community worldwide.");
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;

                    case "3":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Engineer Tomas: I understand, but if we don't act now, the village will be in real trouble. Please reconsider.\"", "Engineer Tomas", ConsoleColor.Yellow);
                        validResponse = true;
                        break;

                    default:
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Please choose a valid option (1, 2, or 3).");
                        break;
                }
            }
        }
        else
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Engineer Tomas:\"Thank you for your help!", "Engineer Tomas", ConsoleColor.Yellow);
            Text.PrintWrappedText("With the pipelines fixed everything maybe the Old Well still has a chance to revive...\"", "Old Well", ConsoleColor.Blue);
            Text.PrintWrappedText("1. I'm glad I could help!", "1", ConsoleColor.Green);
            Text.PrintWrappedText("2. Can you explain how fixing the leaking pipes benefits the village?", "2", ConsoleColor.Green);
            bool validResponse = false;
            while (!validResponse)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        validResponse = true;
                        break;
                    case "2":
                        if (!haschosen2ndPipes)
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Engineer Tomas: By repairing the leaking pipes, we’ve reduced water wastage significantly. This ensures more water reaches homes and farms, supporting both daily needs and agricultural productivity. It also helps conserve water resources for future use.", "Engineer Tomas", ConsoleColor.Yellow);
                            validResponse = true;
                            haschosen2ndPipes = true;
                            game.IncreaseScore(10);
                            break;

                        }
                        else
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Engineer Tomas: You've already asked about this. Let's move on.", "Engineer Tomas", ConsoleColor.Yellow);
                            validResponse = true;
                            break;
                        }
                    default:
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Please choose a valid option (1 or 2).");
                        break;

                }
            }
        }
    }
}