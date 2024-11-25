namespace TextBasedGame.Rooms;

public class CommunityHealthCenter : Room
{

    public CommunityHealthCenter()
    : base("Community Health Center", "A small health center where villagers come for medical treatment. \nThe water crisis has led to a rise in water-borne diseases.")
    {
        RoomNPC = new NPC(
                   "Dr. Eliza",
                   "The villagers are suffering from waterborne diseases. \n We need supplies to help them!",
                   "Keep fighting for clean water. It will save lives."
               );
    }

    public override void GiveItem(Game game, string itemName)
    {
        string normalizedItemName = itemName.Trim().ToLower();

        // Find the item in the player's inventory
        Item foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);

        if (foundItem != null && game.CurrentRoom.RoomNPC != null)
        {
            // Check if the NPC is Dr. Eliza
            if (game.CurrentRoom.RoomNPC.Name == "Dr. Eliza" && (normalizedItemName == "medicinal herbs" || normalizedItemName == "hygiene practice leaflet"))
            {
                Console.Clear();
                Console.WriteLine($"{game.CurrentRoom.RoomNPC.Name} takes the {foundItem.Name}.", foundItem.Name, ConsoleColor.DarkMagenta);
                game.Inventory.Remove(foundItem);

                // Increment the quest item counter
                ((NPC)game.CurrentRoom.RoomNPC).ReceiveItem(foundItem); // Call method to track received items

                // Check if both items have been given
                if (((NPC)game.CurrentRoom.RoomNPC).BothItemsReceived())
                {
                    Console.Clear();
                    Text.PrintWrappedText("Dr. Eliza: \"Thank you for gathering those supplies! With these, we can improve the health of our villagers.\"", "Dr. Eliza", ConsoleColor.DarkYellow);
                    game.CurrentRoom.RoomNPC.CompleteQuest(); // Mark the quest for the NPC as complete
                }
                else
                {
                    Console.Clear();
                    Text.PrintWrappedText("Dr. Eliza: \"Thank you! I still need the other item.\"", "Dr. Eliza", ConsoleColor.DarkYellow);
                }
            }
            else
            {
                Console.Clear();
                Text.PrintWrappedText($"{game.CurrentRoom.RoomNPC.Name} doesn't need this item.", game.CurrentRoom.RoomNPC.Name, ConsoleColor.DarkYellow);
            }
        }
        else
        {
            Console.Clear();
            Text.PrintWrappedText("You don't have that item in your inventory or there's no one here to give it to.");
        }
        string ItemName = itemName.Trim().ToLower();
        foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);
    }
    public override void TalkToNPC(Game game)
    {
        if (NPC.itemsReceived < 2)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Dr. Eliza: The villagers are suffering from waterborne diseases, and we’re running low on supplies to help them.", "Dr. Eliza", ConsoleColor.Yellow);
            Text.PrintWrappedText("If you could gather a Hygiene Practice Leaflet and Medicinal Herbs, it would greatly assist our efforts.\"", "Hygiene Practice Leaflet and Medicinal Herbs", ConsoleColor.DarkMagenta);
            Text.PrintWrappedText("1. \"Where can I find these items?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"Why are these items so important?\"", "2", ConsoleColor.DarkGreen);

            bool validResponse = false;
            while (!validResponse)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Dr. Eliza: The Hygiene Practice Leaflet can be found in this area—it's a guide that teaches villagers how to prevent diseases by washing hands, using clean water, and properly disposing of waste.", "Hygiene Practice Leaflet", ConsoleColor.DarkMagenta);
                        Text.PrintWrappedText("As for the Medicinal Herbs, they grow near the riverbank. These herbs have natural antibacterial properties that can alleviate symptoms of waterborne diseases. They’ve been used for generations in traditional medicine.", "Medicinal Herbs", ConsoleColor.DarkMagenta);
                        Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Once you return use the command give [item].", "give [item]", ConsoleColor.DarkMagenta);
                        Text.PrintWrappedText("Then use the command talk.", "talk", ConsoleColor.DarkMagenta);

                        validResponse = true;
                        break;

                    case "2":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Dr. Eliza: Proper hygiene practices are critical in preventing the spread of waterborne diseases. Without education, even access to clean water might not be enough. The Hygiene Practice Leaflet will help teach the villagers about the importance of sanitation.", "Dr. Eliza", ConsoleColor.Yellow);
                        Text.PrintWrappedText("The Medicinal Herbs are equally essential because they provide immediate relief to those already suffering. While we’re working on improving water quality, these herbs can save lives in the meantime.");
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
        else
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Dr. Eliza: Thank you for solving the problems in this area, Alex! Your help means a lot to our village.", "Dr. Eliza", ConsoleColor.Yellow);

            if (!game.hasWaterTestingKit || !game.hasWaterPurificationTablet) // Only prompt if player hasn't taken it yet
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Dr. Eliza: I have two items that could help you on your journey: a Water Testing Kit to test the water quality or a Water Purification Tablet to purify the contaminated water. Which one would you like to take?\"", "Dr. Eliza", ConsoleColor.Yellow);
                Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Use the command talk again to take the other item.", "talk", ConsoleColor.DarkMagenta);
                Text.PrintWrappedText("1. Water Testing Kit");
                Text.PrintWrappedText("2. Water Purification Tablet");

                string choice = Console.ReadLine().ToLower();

                if (choice == "1")
                {
                    if (!game.hasWaterTestingKit)
                    {
                        Text.PrintSeparator();
                        game.ReceiveItem(new Item("Water Testing Kit", ConsoleColor.DarkMagenta, "A kit used to test water quality."));
                        game.hasWaterTestingKit = true;

                    }
                    else
                    {
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Dr. Eliza: \"You already have the Water Testing Kit.\"");
                    }
                }
                else if (choice == "2")
                {
                    if (!game.hasWaterPurificationTablet)
                    {
                        Text.PrintSeparator();
                        game.ReceiveItem(new Item("Water Purification Tablets", ConsoleColor.DarkMagenta, "Tablets used to purify contaminated water."));
                        game.hasWaterPurificationTablet = true;

                    }
                    else
                    {
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Dr. Eliza: \"You already have the Water Purification Tablets.\"");
                    }
                }
                else
                {
                    Text.PrintSeparator();
                    Text.PrintWrappedText("Please choose either option 1 or 2.");
                }
            }

            else
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("You've already chosen an item. Keep up the good work!\"");
            }
        }
    }
}