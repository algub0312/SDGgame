namespace TextBasedGame.Rooms;

public class ContaminatedWaterStorage : Room
{
    public static bool hasCleanedTanks = false;
    public static bool haschosen2nd = false;
    public ContaminatedWaterStorage()
: base("Contaminated Water Storage Tanks", "Large, old tanks filled with water that has not been treated properly. \nThe contamination has spread through the pipes connected to them.")
    {
        RoomNPC = new NPC(
               "Niko",
               "These tanks are the main source of our contamination problems. We need a Water Purification Tablet to clean the water.",
               "The village can’t survive if we don't purify this water."
           );
    }
    public override void UseItem(Game game, string itemName)
    {
        string normalizedItemName = itemName.Trim().ToLower();
        Item foundItem = game.Inventory.Find(item => item.Name.ToLower() == normalizedItemName);

        if (game.CurrentRoom.Name == "Contaminated Water Storage Tanks" && foundItem.Name.ToLower() == "water purification tablets")
        {
            if (!hasCleanedTanks)
            {
                Console.Clear();
                Text.PrintWrappedText("You use the Water Purification Tablets to purify the contaminated water in the tanks. The water is now safe to drink!", "Water Purification Tablets", ConsoleColor.DarkMagenta);
                game.IncreaseScore(20);  // Award points for purifying the water
                (game.CurrentRoom.RoomNPC as NPC).CompleteQuest(); // Mark Niko's quest as complete
                hasCleanedTanks = true;
                Item purifiedWaterCrystal = new Item("Purified Water Crystal", ConsoleColor.DarkMagenta, "A rare, shimmering crystal that has absorbed the pure essence of clean water. It is highly valued by the village for its beauty and rarity.");
                game.ReceiveItem(purifiedWaterCrystal);

                game.hasPurifiedWaterCrystal = true;
                Text.PrintSeparator();
                game.CheckFinalItems(game);
            }
            else
            {
                Text.PrintWithColor("You already cleaned the tanks.", ConsoleColor.Yellow);
            }
        }
    }
    public override void TalkToNPC(Game game)
    {
        if (!hasCleanedTanks)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Niko: These tanks are filled with contaminated water.", "Niko", ConsoleColor.Yellow);
            Text.PrintWrappedText("We need Water Purification Tablets to clean them.\"", "Water Purification Tablets", ConsoleColor.DarkMagenta);
            Text.PrintWrappedText("1. \"Where can I find the Water Purification Tablet?\"", "1", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("2. \"How bad is the contamination?\"", "2", ConsoleColor.DarkGreen);
            Text.PrintWrappedText("3. \"I don't think I can help with this.\"", "3", ConsoleColor.DarkGreen);

            bool validResponse = false;
            while (!validResponse)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("The Water Purification Tablet can be found in the Community Health Center, but first you need to help Dr. Eliza, I'm sure she will be grateful for your help. Then come back and use\"", "Community Health Center", ConsoleColor.Blue);
                        validResponse = true;
                        break;

                    case "2":

                        Text.PrintSeparator();
                        Text.PrintWrappedText("Niko: The contamination is severe. It’s caused by runoff from nearby farms and improper waste disposal.");
                        Text.PrintWrappedText("The tanks were meant to store clean rainwater, but pollutants have been seeping into them.");
                        Text.PrintWrappedText("Drinking this water without purification can lead to diseases like cholera and dysentery.");
                        Text.PrintWrappedText("Cleaning these tanks isn’t just about drinking water—it’s about safeguarding the entire village’s health.");
                        validResponse = true;
                        game.IncreaseScore(10);
                        break;


                    case "3":
                        Text.PrintSeparator();
                        Text.PrintWrappedText("Niko: I understand, but without your help, the village will continue to suffer.");
                        Text.PrintWrappedText("Please reconsider when you have time. Clean water is a basic need, and we can’t do this alone.");
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

            Text.PrintWrappedText("Niko: Thank you for helping purify the water. This will make a huge difference for the village!\"", "Niko", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. I'm glad I could help!", "1", ConsoleColor.Green);
            Text.PrintWrappedText("2. Can you explain how repairing the water tanks benefits the village?", "2", ConsoleColor.Green);
            bool validResponse = false;
            while (!validResponse)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Text.PrintSeparator();
                        validResponse = true;
                        break;

                    case "2":
                        if (!haschosen2nd)
                        {


                            Text.PrintSeparator();
                            Text.PrintWrappedText("Niko: Certainly! The repaired water tanks ensure a reliable supply of clean water, which reduces the risk of waterborne diseases like cholera and dysentery. Clean water also improves hygiene, supports agriculture by providing irrigation, and boosts the overall well-being of the community.", "Niko", ConsoleColor.Yellow);
                            game.IncreaseScore(10);
                            validResponse = true;
                            haschosen2nd = true
                            break;



                        }
                        else
                        {
                            Text.PrintSeparator();
                            Text.PrintWrappedText("Niko: You've already asked about this. Let's move on.", "Niko", ConsoleColor.Yellow);
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