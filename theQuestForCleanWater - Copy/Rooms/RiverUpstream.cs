namespace TextBasedGame.Rooms;

public class RiverUpstream : Room
{
    public static bool hasFishChoice = false;
    public static bool hasCleanedRiver { get; set; } = false; // Track if the river has been cleaned
    public static bool hasFishingNet = false; // Track if the player has received the Fishing Net
    public static bool hasFish = false;
    public static bool FishRodBroken = false;
    public static bool WantsToFish = false;
    public int ProbFish { get; set; }
    public bool StopChat = false;
    public RiverUpstream()
    : base("River Upstream", "A river that is the primary water source for the village, though upstream pollution is affecting water quality.")
    {
        RoomNPC = new NPC(
           "Jamal",
           "The river is filled with trash and pollutants. I can't catch any fish, and the village suffers.",
           "Thank you for helping clean the river. Here, take this Fishing Net. It might help you."
       );
    }
    public override void UseItem(Game game, string itemName)
    {
        string normalizedItemName = itemName.Trim().ToLower();
        Item foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);

        if (game.CurrentRoom.Name == "River Upstream" && foundItem.Name.ToLower() == "fishing net")
        {

            if (!hasCleanedRiver)
            {
                while (!hasCleanedRiver)
                {

                    Text.PrintWrappedText("Do you want to fish for debris or stop? [fish/stop] ", "fish", ConsoleColor.DarkMagenta);
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "fish")
                    {
                        CatchDebris();
                        game.IncreaseScore(5);
                    }
                    else if (choice == "stop")
                    {
                        Console.WriteLine("You decided to stop fishing for now.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid command.");

                    }
                }
            }

            if (hasCleanedRiver && !FishRodBroken && !StopChat)
            {
                Console.WriteLine("Do you want to fish for a fish now? [yes/no]");
                string fishChoice = Console.ReadLine().ToLower();
                if (fishChoice == "yes")
                {
                    TryFishing(game);
                }
                else
                {
                    Console.WriteLine("You decided not to fish right now");
                }
            }
            else if (hasCleanedRiver && FishRodBroken)
            {
                Text.PrintWithColor("You should visit the Mayor for a new fishing net.", ConsoleColor.Yellow);
            }
            else
            {
                Text.PrintWithColor("The river is already clean.", ConsoleColor.Yellow);
            }
        }
        else
        {
            Text.PrintWithColor("The river is already clean.", ConsoleColor.Yellow);
        }
    }



    private int debrisCount = 0;
    private Random random = new Random();
    private void CatchDebris()
    {
        Console.Clear();
        string[] debrisItems = { "a plastic bottle", "an old boot", "a rusty can", "a broken fishing net" };
        Text.PrintWrappedText($"You pull out {debrisItems[random.Next(debrisItems.Length)]} from the river.");

        debrisCount++;
        Text.PrintWrappedText($"You have removed {debrisCount} pieces of junk from the river.");

        if (debrisCount >= 3)
        {
            Console.Clear();
            hasCleanedRiver = true;
            Text.PrintWrappedText("The river is now clean! You can attempt to catch a fish.", "The river is now clean!", ConsoleColor.Green);
        }
        else
        {
            Text.PrintWrappedText("There is still some junk left in the river.");
        }
    }
    private void CatchFish()
    {
        Console.Clear();
        Console.WriteLine("You cast the rod into the clean river...");
        Console.WriteLine("After a few moments, you reel in a fish!");
    }
    private void TryFishing(Game game)
    {
        Random random = new Random();
        int outcome = random.Next(1, 4); // Generates a random number between 1 and 3

        switch (outcome)
        {
            case 1:
                if (game.Inventory.Count > 0)
                {
                    FishRodBroken = true;
                    Item removedItem = game.Inventory[random.Next(game.Inventory.Count)];
                    hasFishingNet = false;
                    Console.WriteLine($"Oh no! It looks like your Fishing Net broke");
                    Console.WriteLine($"you should visit the Mayor to give you help.");

                }
                break;

            case 2:
                Console.WriteLine("You tried fishing but didn't catch anything.");
                break;



            case 3:
                if (game.CurrentRoom.RoomNPC is NPC npc)
                    npc.CompleteQuest();
                game.IncreaseScore(10);
                Item fish = new Item("Fish", ConsoleColor.DarkMagenta, "A fresh fish caught from the river.");
                game.Inventory.Add(fish);
                Text.PrintWrappedText("You also catch a fish while cleaning the river.", "fish", ConsoleColor.DarkMagenta);
                hasFish = true;
                StopChat = true;
                Text.PrintSeparator();
                game.CheckFinalItems(game);
                break;
        }
    }
    public override void TalkToNPC(Game game)
    {
        if (hasCleanedRiver == false)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Jamal: The river is filled with trash and pollutants. I can't catch any fish, and the village suffers.\"", "Jamal", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. \"How can I help clean the river?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"Where is all this pollution coming from?\"", "2", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("3. \"I don't have time for this.\"", "3", ConsoleColor.DarkGreen);

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
                        Text.PrintWrappedText("Jamal: If you can help me remove the trash from the river, it would make a huge difference. Here, take this Fishing Net to help filter out the debris.\"", "Jamal", ConsoleColor.Yellow);
                        if (!hasFishingNet)
                        {
                            game.Inventory.Add(new Item("Fishing Net", ConsoleColor.DarkMagenta, "A net used to catch fish or filter debris from water."));
                            hasFishingNet = true;
                            Text.PrintWrappedText("You receive a Fishing Net from Jamal. Type", "Fishing Net", ConsoleColor.DarkMagenta);
                                        Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Use the command use fishing net.", "use fishing net", ConsoleColor.DarkMagenta);

                        }
                        validResponse = true;
                        break;

                    case "2":

                        Text.PrintSeparator();
                        Text.PrintWrappedText("Jamal: Most of the pollution comes from careless villagers throwing waste near the riverbanks and upstream industries releasing untreated waste.");
                        Text.PrintWrappedText("When trash and chemicals enter the water, it not only makes it unsafe for us to use but also destroys the habitat for fish and other aquatic life.");
                        Text.PrintWrappedText("The river is the lifeblood of our village. Without clean water, crops fail, fish populations drop, and people fall ill. Cleaning it up now will protect our future.");
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;


                    case "3":

                        Text.PrintSeparator();
                        Text.PrintWrappedText("Jamal: I understand, but the river's health affects us all. If we ignore the problem now, it will only get worse. Please reconsider when you have time.");
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
            Text.PrintWrappedText("Jamal: Thank you for helping clean the river. The water looks much better now!\"", "Jamal", ConsoleColor.Yellow);
            Text.PrintWrappedText("Now I can fish again and bring my family food.");
            Text.PrintWrappedText("You can catch a fish too by usin the command use fishing net.", "use fishing net", ConsoleColor.DarkMagenta);

            if (!game.Inventory.Exists(item => item.Name.ToLower() == "fishing net"))
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Jamal: Here, take this Fishing Net. It might help you.\"", "Jamal", ConsoleColor.Yellow);
                game.Inventory.Add(new Item("Fishing Net", ConsoleColor.DarkMagenta, "A net used to catch fish or filter debris from water."));
                hasFishingNet = true;
                Text.PrintWrappedText("You receive a Fishing Net from Jamal. Use it to clean the river.", "Fishing Net", ConsoleColor.DarkMagenta);
            }
        }
    }
}