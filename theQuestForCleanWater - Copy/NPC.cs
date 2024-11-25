
using System.Configuration.Assemblies;
using System.Runtime.ConstrainedExecution;
using TextBasedGame.Rooms;
public class NPC
{
    public string Name { get; set; }
    public string InitialDialogue { get; set; }
    public string FollowUpDialogue { get; set; }
    public bool HasCompletedQuest { get; set; } = false;  // Track if the NPC's quest has been completed
    public static int itemsReceived = 0; // Track how many required items have been received
    private bool hasWaterTestingKit;
    private bool hasWaterPurificationTablet;



    public NPC(string name, string initialDialogue, string followUpDialogue)
    {
        Name = name;
        InitialDialogue = initialDialogue;
        FollowUpDialogue = followUpDialogue;
    }

    public void ComplexDialogue(Game game)
    {
        if (game.CurrentRoom == null)
        {
            Console.WriteLine("There is no one to talk to here.");
        }
    }


    public void Talk(Game game)
    {
        if (Name == "Dr. Eliza" && !HasCompletedQuest)
        {
            ComplexDialogue(game);  // Evaluate the current state when talking
        }
        else
        {

            if (Name == "Dr. Eliza" && (!game.hasWaterTestingKit || !game.hasWaterPurificationTablet)) // Only prompt if player hasn't taken it yet
            {
                Text.PrintSeparator();
                Text.PrintWrappedText($"{Name}: \"Thank you for the help! Would you like take something from the Community Health Centre?\"");
                Text.PrintWrappedText("1. \"Water Testing Kit\"", "1", ConsoleColor.DarkGreen);
                Text.PrintWrappedText("2. \"Water purification tablet\"", "2", ConsoleColor.DarkGreen);

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
                                game.ReceiveItem(new Item("Water Testing Kit", ConsoleColor.DarkMagenta, "A kit used to test water quality."));
                                game.hasWaterTestingKit = true;


                                validResponse = true;
                                break;
                            }
                            else
                            {
                                Text.PrintSeparator();
                                Text.PrintWrappedText("You already have the Water Testing Kit.\"", "Water Testing Kit", ConsoleColor.DarkMagenta);
                                validResponse = true;
                                break;
                            }

                        case "2":

                            if (!game.hasWaterPurificationTablet)
                            {
                                Text.PrintSeparator();
                                game.ReceiveItem(new Item("Water Purification Tablets", ConsoleColor.DarkMagenta, "Tablets used to purify contaminated water."));
                                game.hasWaterPurificationTablet = true;

                                validResponse = true;
                                break;

                            }
                            else
                            {
                                Text.PrintSeparator();
                                Text.PrintWrappedText("You already have the Water Purification Tablets.\"", "Water Purification Tablets", ConsoleColor.DarkMagenta);
                                validResponse = true;
                                break;
                            }

                    }
                }
            }
        }
        if (Name == "Dr. Eliza" && game.hasWaterTestingKit && game.hasWaterPurificationTablet)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Dr. Eliza: \"You already have all the items available to get from me. Thank you for the help!\"", "Dr. Eliza", ConsoleColor.Yellow);
        }

        if (Name == "Niko" && ContaminatedWaterStorage.hasCleanedTanks)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Niko: Thank you for the help, I'm sure that the village is in good hands!", "Niko", ConsoleColor.Yellow);
        }
        if (Name == "Jamal" && RiverUpstream.hasFish)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Jamal: Thank you, now I can fish again and bring my family food.", "Jamal", ConsoleColor.Yellow);
        }


    }
    public void CompleteQuest()
    {
        HasCompletedQuest = true;
    }

    public void ReceiveItem(Item item)
    {
        if (item.Name.ToLower() == "hygiene practice leaflet" || item.Name.ToLower() == "medicinal herbs")
        {
            itemsReceived++; // Increment the count of received items
        }
    }

    public bool BothItemsReceived()
    {
        return itemsReceived == 2; // Check if both items have been received
    }

    // New method to provide hints when talking to Old Sara
    public static void ProvideHints(Game game)
    {
        if (!LeakingPipelines.HasFixedPipelines)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: \"You must fix the leaking pipelines first to ensure clean water reaches the well.\"", "Old Sara", ConsoleColor.Yellow);
        }
        else
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: \"Now that the pipelines are fixed, we can focus on testing the well water to ensure its safety.\"", "Old Sara", ConsoleColor.Yellow);
        }
    }
    public static void OldSaraDiscussPurificationChoice(Game game)
    {
        if (!RiverUpstream.hasCleanedRiver)
        {
            Text.PrintSeparator();
            Text.PrintWrappedText("Old Sara: \"The water in the well is still contaminated. You have two options, Alex.\"", "Old Sara", ConsoleColor.Yellow);
            Text.PrintWrappedText("1. Clean the river upstream to address the root cause.", "Clean the river upstream", ConsoleColor.Red);
            Text.PrintWrappedText("2. Use Water Purification Tablets in the well to purify the water directly, but it’s a temporary solution.", "Use Water Purification Tablets", ConsoleColor.Red);

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Old Sara: \"Good choice. Clean the river to ensure a long-term solution.\"", "Old Sara", ConsoleColor.Yellow);
                // Offer the option to clean the river
                DryWell.decision = true;
            }
            else if (choice == "2")
            {
                Text.PrintSeparator();
                Text.PrintWrappedText("Old Sara: \"Using the tablets will help, but it’s not as effective as cleaning the river.\"", "Old Sara", ConsoleColor.Yellow);
                          Text.PrintWrappedText("Hint:", "Hint", ConsoleColor.DarkMagenta); Text.PrintWrappedText("Use the command use water purification tablets.", "water purification tablets.", ConsoleColor.DarkMagenta);

                // Allow the use of purification tablets as a fallback
            }
        }

    }




}
