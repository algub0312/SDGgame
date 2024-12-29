using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
namespace TextBasedGame.Rooms;

public class RainwaterHarvestingSystem : Room
{
    public static bool hasRepairedSystem = false;
    private System.Timers.Timer rainwaterTimer;
    private int remainingTime = 120;
    public static bool isRainwaterQuestActive = false;
    public bool hasFilter = false;
    public bool installed = false;
    public static bool hasJarofRainwater = false;
    public bool haschosen2ndRainwater = false;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public RainwaterHarvestingSystem()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    : base("Rainwater Harvesting System", "A system that collects rainwater but is currently clogged and neglected.")
    {
        RoomNPC = new NPC(
        "Amara",
        "The rainwater system is clogged and neglected...",
        "Once it's cleaned, the village can have fresh water again."
        );
    }
    public override void UseItem(Game game, string itemName)

    {
        string normalizedItemName = itemName.Trim().ToLower();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Item foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (game.CurrentRoom.Name == "Rainwater Harvesting System" && foundItem.Name.ToLower() == "filter")
        {
            Console.Clear();
            Text.PrintWrappedText("You approach the rainwater system to install the new filter.");

            // Step-by-step instructions

            int step = 1;

            while (!installed)
            {
                switch (step)
                {
                    case 1:
                        Console.Clear();
                        Text.PrintWrappedText("Step 1: Place the filter in the slot. Type 'place' to continue.", "place", ConsoleColor.Green);
                        if (Console.ReadLine()?.ToLower() == "place")
                        {
                            Console.WriteLine("The filter is in place.");
                            step++;
                        }
                        else
                        {
                            Console.WriteLine("You must type 'place' to continue.", "place", ConsoleColor.Green);
                        }
                        break;

                    case 2:
                        Console.Clear();
                        Text.PrintWrappedText("Step 2: Rotate the filter to lock it in. Type 'rotate' to continue.", "rotate", ConsoleColor.Green);
                        if (Console.ReadLine()?.ToLower() == "rotate")
                        {
                            Console.WriteLine("The filter clicks into place.");
                            step++;
                        }
                        else
                        {
                            Console.WriteLine("You must type 'rotate' to continue.", "rotate", ConsoleColor.Green);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Text.PrintWrappedText("Step 3: Tighten the screws. Type 'tighten' to secure the filter.", "tighten", ConsoleColor.Green);
                        if (Console.ReadLine()?.ToLower() == "tighten")
                        {
                            Console.WriteLine("The filter is now secured in the system.");
                            installed = true;
                            hasRepairedSystem = true;

                        }
                        else
                        {
                            Text.PrintWrappedText("You must type 'tighten' to complete the installation.", "tighten", ConsoleColor.Green);
                        }
                        break;
                }
            }
            Console.Clear();
            Text.PrintWithColor("The filter is now fully installed!", ConsoleColor.Green);
            rainwaterTimer.Stop();
            Text.PrintWrappedText("Amara: You completed the quest, good job! The rain should start in any moment. If the Harvesting system works properly you will be rewarded.", "Amara", ConsoleColor.Yellow);
            game.ReceiveItem(new Item("Jar of Fresh Rainwater", ConsoleColor.DarkMagenta, "Fresh rainwater collected from the harvesting system."));
            hasJarofRainwater = true;
            Text.PrintSeparator();
            game.CheckFinalItems(game);
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public void StartRainwaterQuestTimer()
    {
        rainwaterTimer = new System.Timers.Timer(1000); // Set to tick every 1 second
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        rainwaterTimer.Elapsed += OnRainwaterTimedEvent;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        rainwaterTimer.AutoReset = true; // Ensure it continues until manually stopped
        rainwaterTimer.Start(); // Start the timer

    }


    private void OnRainwaterTimedEvent(object sender, ElapsedEventArgs e)
    {
        if (remainingTime > 0)
        {
            remainingTime--;

            // Save the current cursor position
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Calculate the position for the bottom-right corner
            int timerLeft = Console.WindowWidth - 20; 
            int timerTop = Console.WindowHeight - 1;

            Console.SetCursorPosition(timerLeft, timerTop);
            Console.Write(new string(' ', 20)); // Clear the timer area with spaces

            // Write the updated timer
            Console.SetCursorPosition(timerLeft, timerTop);
            Console.Write($"Time remaining: {remainingTime}s");

            // Restore the previous cursor position
            Console.SetCursorPosition(currentLeft, currentTop);
        }
        else if (remainingTime >= 0 && installed == true)
        {
            rainwaterTimer.Stop();
            isRainwaterQuestActive = false;

            // Save the current cursor position
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Calculate the position for the bottom-right corner
            int timerLeft = Console.WindowWidth - 20; // Reserve 20 characters for the timer display
            int timerTop = Console.WindowHeight - 1;

            Console.SetCursorPosition(timerLeft, timerTop);
            Console.Write(new string(' ', 20)); // Clear the timer area with spaces

            // Write the failure message
            Console.SetCursorPosition(timerLeft, timerTop);


            // Restore the previous cursor position
            Console.SetCursorPosition(currentLeft, currentTop);
        }
        else
        {
            rainwaterTimer.Stop();
            isRainwaterQuestActive = false;

            // Save the current cursor position
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Calculate the position for the bottom-right corner
            int timerLeft = Console.WindowWidth - 20; // Reserve 20 characters for the timer display
            int timerTop = Console.WindowHeight - 1;

            Console.SetCursorPosition(timerLeft, timerTop);
            Console.Write(new string(' ', 20)); // Clear the timer area with spaces

            // Write the failure message
            Console.SetCursorPosition(timerLeft, timerTop);
            Console.Write("Time's up!");

            // Restore the previous cursor position
            Console.SetCursorPosition(currentLeft, currentTop);
        }
    }



    public void StartRainwaterQuest(Game game)
    {
        if (isRainwaterQuestActive)
        {
            Text.PrintWrappedText("You are already in the middle of this quest!");
            return;
        }
        else
        {

            Text.PrintWrappedText("The rain is almost starting!\nYou now have 120 seconds left to repair the system.\nAre you ready to start? (yes/no)", "120 seconds", ConsoleColor.Red);
            bool validResponse = false;
            while (!validResponse)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                switch (choice)
                {
                    case "yes":
                        isRainwaterQuestActive = true;
                        validResponse = true;
                        remainingTime = 120; // Reset timer
                        StartRainwaterQuestTimer();
                        Text.PrintWrappedText("The rainwater filter is clogged and needs replacement. I've heard that the most known engineer in this town owns a new filter in his storage! Here, I think this is his key, he forgot it the last time he tried to repair the machine.", "most known engineer in this town", ConsoleColor.Red);
                        Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Once you return use the command use filter.", "use filter", ConsoleColor.DarkMagenta);

                        if (!game.hasStorageKey)
                        {
                            game.ReceiveItem(new Item("Storage key", ConsoleColor.DarkMagenta, "A key to access Engineer Tomas's Workshop."));
                            game.hasStorageKey = true;

                        }
                        break;

                    case "no":
                        validResponse = true;
                        Text.PrintWrappedText("Amara: I understand that you're not ready yet, you can start again later.", "Amara", ConsoleColor.Yellow);
                        break;

                    default:
                        Console.WriteLine("Please choose a valid option (yes or no).");
                        break;
                }
            }
        }
    }
    public override void TalkToNPC(Game game)
    {
        if (!hasRepairedSystem)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Amara: Hello there, traveler! I'm Amara, the keeper of the Rainwater Harvesting System.", "Amara", ConsoleColor.Yellow);
            Text.PrintWrappedText("Unfortunately, it's not working as it should. This system is crucial for collecting rainwater, which is one of the cleanest and most reliable sources we have.");
            Text.PrintWrappedText("Without it, we risk relying on contaminated water sources, leading to health problems like waterborne diseases and agricultural losses. Can you help us fix it?");
            Text.PrintWrappedText("1. \"What’s the problem?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"Sure, I’ll help. What do I need to do\"", "2", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("3. \"Why don’t you just use another source of water?\"", "3", ConsoleColor.DarkGreen);

            bool validResponse = false;
            while (!validResponse)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string choice = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                switch (choice)
                {
                    case "1":
                        if (!isRainwaterQuestActive)
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Amara: Ah, a curious one! Well, the issue lies with the missing filter in the rainwater harvesting system.", "Amara", ConsoleColor.Yellow);
                            Text.PrintWrappedText("Without the filter, the rainwater we collect can’t be purified properly. This means the water could be contaminated and unsafe for drinking or storage.");
                            Text.PrintWrappedText("Clean water is vital for our health and the well-being of our crops. Without it, our village becomes vulnerable to waterborne diseases and food shortages.");
                            validResponse = true;
                            StartRainwaterQuest(game);
                            break;

                        }
                        else
                        {
                            validResponse = true;
                            Text.PrintWrappedText("You are already in the middle of this quest!");
                            break;
                        }


                    case "2":
                        if (!isRainwaterQuestActive)
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Amara: Thank you! Here’s what we need you to do:", "Amara", ConsoleColor.Yellow);
                            Text.PrintWrappedText("Find a replacement filter. It’s crucial to ensure the rainwater is safe for drinking and storage.");
                            Text.PrintWrappedText("The filter ensures that impurities like dust, bacteria, and other harmful particles are removed before the water is stored.");
                            Text.PrintWrappedText("Please act quickly—if we miss the next rainfall, we won’t have enough water to sustain the village.");
                            validResponse = true;
                            StartRainwaterQuest(game);
                            break;
                        }
                        else
                        {
                            validResponse = true;
                            Text.PrintWrappedText("You are already in the middle of this quest!");
                            break;
                        }

                    case "3":
                        Text.PrintSeparator();


                        Text.PrintWrappedText("Amara: I wish it were that simple. Most of our alternative sources, like the nearby river, are polluted.", "Amara", ConsoleColor.Yellow);
                        Text.PrintWrappedText("Using contaminated water leads to diseases like cholera and dysentery, which have already caused harm in our village.");
                        Text.PrintWrappedText("Additionally, unreliable sources make us vulnerable to droughts. Without this rainwater harvesting system, our ability to survive here is at great risk.");
                        Text.PrintWrappedText("Rainwater is one of the cleanest and most sustainable sources we have. That’s why restoring this system is so important.");
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;


                    default:
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Please choose a valid option (1, 2, or 3).");
                        break;
                }
            }
        }
        else if (hasJarofRainwater)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Amara: Thank you for cleaning the system! Our village is already noticing the improvement.", "Amara", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. I'm glad I could help!", "1", ConsoleColor.Green);
            Text.PrintWrappedText("2. Can you explain how repairing the system benefits the village?", "2", ConsoleColor.Green);
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
                        if (!haschosen2ndRainwater)
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Amara: Repairing the rainwater harvesting system ensures that we can efficiently collect and store rainwater. This reduces reliance on other water sources, especially during dry periods. It also promotes sustainable water use and helps mitigate water scarcity.", "Amara", ConsoleColor.Yellow);
                            validResponse = true;
                            haschosen2ndRainwater = true;
                            game.IncreaseScore(10);
                            break;
                        }
                        else
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Amara: You've already asked about this. Let's move on.", "Amara", ConsoleColor.Yellow);
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