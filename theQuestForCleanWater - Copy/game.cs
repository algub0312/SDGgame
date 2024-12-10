using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using TextBasedGame;
using TextBasedGame.Rooms;
// Game class
public class Game
{
    private RiverUpstream riverUpstream;


    public Room CurrentRoom { get; set; }
    public List<Item> Inventory { get; set; } = new List<Item>();
    public int Score { get; set; } = 0;
    public bool hasMap = false;  // Boolean to track if the player has received the map

    public bool hasWaterTestingKit = false; // Track if the player has the Water Testing Kit
    //rainwater harvesting system


    public bool hasWaterPurificationTablet = false;  // Track if the player has received the Water Purification Tablet
    public bool isQuestActice = false; // rainwater system

    public bool hasPurifiedWaterCrystal { get; set; } = false;
    public bool hasFinalItems = false;




    public bool puzzleSolved = false;

    public bool hasStorageKey = false;
    public bool requiresKey = false;


    public bool Final = true;

    public Parser Parser { get => parser; set => parser = value; }

    private Dictionary<string, Room> rooms;
    private Parser parser = new Parser();  // Add the parser

    public Game()
    {
        InitializeGame();

    }

    private void InitializeGame()
    {
        // Creating items
        Item wrench = new Item("Wrench", ConsoleColor.DarkMagenta, "A sturdy tool needed for fixing pipes.");
        Item villageMap = new Item("Village Map", ConsoleColor.DarkMagenta, "A map showing all key locations in Aquara.");
        Item rustyBucket = new Item("Rusty Bucket", ConsoleColor.DarkMagenta, "An old bucket, seems useful for collecting water or other items.");
        Item hygieneLeaflet = new Item("Hygiene Practice Leaflet", ConsoleColor.DarkMagenta, "A pamphlet on hygiene practices to prevent disease.");
        Item medicinalHerbs = new Item("Medicinal Herbs", ConsoleColor.DarkMagenta, "Herbs used for treating common ailments.");
        Item waterTestingKit = new Item("Water Testing Kit", ConsoleColor.DarkMagenta, "A kit used to test water quality.");
        Item fishingNet = new Item("Fishing Net", ConsoleColor.DarkMagenta, "A net used to catch fish or filter debris from water.");
        Item fish = new Item("Fish", ConsoleColor.DarkMagenta, "A fresh fish caught from the river.");
        Item storageKey = new Item("Storage Key", ConsoleColor.DarkMagenta, "A key to access the storage tanks.");
        Item jarRainwater = new Item("Jar of Fresh Rainwater", ConsoleColor.DarkMagenta, "Fresh rainwater collected from the harvesting system.");
        Item waterPurificationTablet = new Item("Water Purification Tablet", ConsoleColor.DarkMagenta, "A tablet used to purify contaminated water.");
        Item purifiedWaterCrystal = new Item("Purified Water Crystal", ConsoleColor.DarkMagenta, "A rare, shimmering crystal that has absorbed the pure essence of clean water. It is highly valued by the village for its beauty and rarity.");
        Item filter = new Item("Filter", ConsoleColor.DarkMagenta, "This looks like it would be useful for the Rainwater Harvesting System");



        // Creating rooms
        Room villageTownHall = new VillageTownHall();
        villageTownHall.Items.Add(wrench);

        Room dryWell = new DryWell();


        Room communityHealthCenter = new CommunityHealthCenter();
        communityHealthCenter.Items.Add(hygieneLeaflet);

        Room leakingPipelines = new LeakingPipelines();

        Room riverUpstream = new RiverUpstream();
        riverUpstream.Items.Add(medicinalHerbs);

        Room contaminatedWaterStorage = new ContaminatedWaterStorage();

        Room rainwaterHarvestingSystem = new RainwaterHarvestingSystem();

        Room workshop = new Room("Engineer Tomas's Workshop", "A small room with little light, use the explore command to search for things.");
        workshop.Items.Add(filter);

        // Connecting rooms
        villageTownHall.Exits.Add("east", dryWell);
        villageTownHall.Exits.Add("north", communityHealthCenter);
        villageTownHall.Exits.Add("south", leakingPipelines);
        villageTownHall.Exits.Add("west", riverUpstream);

        dryWell.Exits.Add("west", villageTownHall);
        communityHealthCenter.Exits.Add("south", villageTownHall);
        leakingPipelines.Exits.Add("north", villageTownHall);
        leakingPipelines.Exits.Add("south", contaminatedWaterStorage); // Fixed connection to Contaminated Water Storage Tanks
        leakingPipelines.Exits.Add("east", rainwaterHarvestingSystem);
        leakingPipelines.Exits.Add("west", workshop);
        riverUpstream.Exits.Add("east", villageTownHall);
        contaminatedWaterStorage.Exits.Add("north", leakingPipelines);
        rainwaterHarvestingSystem.Exits.Add("west", leakingPipelines);
        workshop.Exits.Add("east", leakingPipelines);

        // Set the starting room
        CurrentRoom = villageTownHall;

        rooms = new Dictionary<string, Room>
            {
                { "Village Town Hall", villageTownHall },
                { "Dry Well", dryWell },
                { "Community Health Center", communityHealthCenter },
                { "Leaking Pipelines", leakingPipelines },
                { "River Upstream", riverUpstream },
                { "Contaminated Water Storage Tanks", contaminatedWaterStorage },
                { "Rainwater Harvesting System", rainwaterHarvestingSystem }
            };
    }


    public void Start()
    {
        Console.Clear();
        Text.center(" _______ .-. .-.,---.     .---. .-. .-.,---.     .---.  _______   .---.  ,---.  ", ConsoleColor.DarkCyan);
        Text.center("|__   __|| | | || .-'    ( .-. \\| | | || .-'    ( .-._)|__   __| / .-. ) | .-'  ", ConsoleColor.DarkCyan);
        Text.center("  )| |   | `-' || `-.   (_)| | || | | || `-.   (_) \\     )| |    | | |(_)| `-.  ", ConsoleColor.DarkCyan);
        Text.center(" (_) |   | .-. || .-'    | ||\\ || | | || .-'   _  \\ \\   (_) |    | | | | | .-'  ", ConsoleColor.DarkCyan);
        Text.center("   | |   | | |)||  `--.  \\ `-\\/| `-')||  `--.( `-'  )    | |    \\ `-' / | |    ", ConsoleColor.DarkCyan);
        Text.center("   `-'   /(  (_)/( __.'   `---\\|`---(_)/( __.' `----'     `-'     )---'  )\\|    ", ConsoleColor.DarkCyan);
        Text.center("        (__)   (__)                   (__)                       (_)    (__)    ", ConsoleColor.DarkCyan);
        Text.center("    ,--,  ,-.    ,---.    .--.  .-. .-. .-.  .-.  .--.  _______ ,---.  ,---.    ", ConsoleColor.DarkCyan);
        Text.center("  .' .')  | |    | .-'   / /\\ \\ |  \\| | | |/\\| | / /\\ \\|__   __|| .-'  | .-.\\   ", ConsoleColor.DarkCyan);
        Text.center("  |  |(_) | |    | `-.  / /__\\ \\|   | | | /  \\ |/ /__\\ \\ )| |   | `-.  | `-'/   ", ConsoleColor.DarkCyan);
        Text.center("  \\  \\    | |    | .-'  |  __  || |\\  | |  /\\  ||  __  |(_) |   | .-'  |   (    ", ConsoleColor.DarkCyan);
        Text.center("   \\  `-. | `--. |  `--.| |  |)|| | |)| |(/  \\ || |  |)|  | |   |  `--.| |\\ \\   ", ConsoleColor.DarkCyan);
        Text.center("    \\____\\|( __.'/( __.'|_|  (_)/(  (_) (_)   \\||_|  (_)  `-'   /( __.'|_| \\)\\  ", ConsoleColor.DarkCyan);
        Text.center("          (_)   (__)           (__)                            (__)        (__) ", ConsoleColor.DarkCyan);

        // Brief introduction to the village
        Text.PrintSeparator();
        Text.PrintWrappedText("Welcome to Aquara, a village once brimming with life and laughter, now haunted by the dry, unforgiving dust of its water crisis. Streams have slowed to a trickle, wells have turned sour, and villagers speak in whispers of the days when every drop was clear and pure. You are Alex, a resilient soul born of this land, stepping forward to unravel the village’s problems. Can you restore the water’s flow and bring Aquara back to life?", "Alex", ConsoleColor.Yellow);
        Text.PrintSeparator();
        ShowHelp();
        // Show help commands only at the start or when the player asks for help


        Text.PrintWrappedText("Type 'play' to begin your journey through Aquara.", "play", ConsoleColor.DarkCyan);

        bool isPlaying = false;

        // Wait for the player to type "play" to begin the game
        while (!isPlaying)
        {
            string input = Console.ReadLine()?.ToLower();
            if (input == "play")
            {
                isPlaying = true;
                Console.Clear();
                Text.PrintSeparator();
                Text.PrintWrappedText("\nYou find yourself standing in the Village Town Hall surrounded by concerned villagers and the stern figure of Mayor Anwar.", "Village Town Hall", ConsoleColor.Blue);
                Text.PrintWrappedText("Your quest is to help restore clean water to the village.");
                CurrentRoom.Enter();
            }
            else
            {
                Console.Clear();
                Text.PrintWrappedText("Please type 'play' to start the game.");
            }
        }

        // Main game loop to explore, interact, and navigate
        GameLoop();
    }

    // Main game loop to allow continuous exploration and navigation
    private void GameLoop()
    {
        bool playing = true;

        while (playing)
        {
            // Waiting for the player's input
            string input = Console.ReadLine()?.ToLower();

            // Use the parser to interpret the command
            Command command = Parser.ParseCommand(input);

            if (command != null)
            {
                switch (command.Verb)
                {
                    case "explore":
                        Console.Clear();
                        Text.PrintSeparator();

                        CurrentRoom.ShowItems();
                        break;

                    case "take":
                        Console.Clear();
                        Text.PrintSeparator();
                        if (!string.IsNullOrEmpty(command.Object))
                        {
                            TakeItem(command.Object);
                        }
                        else
                        {
                            Console.Clear();
                            Text.PrintWrappedText("Please specify the item you'd like to take.");
                        }
                        break;

                    case "go":
                        if (!string.IsNullOrEmpty(command.Object))
                        {
                            MoveToRoom(command.Object);
                        }
                        else
                        {
                            Console.Clear();
                            Text.PrintWrappedText("Please specify a direction (e.g., 'go east').");
                        }
                        break;

                    case "talk":
                        if (CurrentRoom != null)
                        {
                            CurrentRoom.TalkToNPC(this);  // Pass the game context to the NPC for interaction
                        }
                        else
                        {
                            Console.Clear();
                            Text.PrintWrappedText("There's no one here to talk to.");
                        }
                        break;

                    case "use":
                        Console.Clear();
                        Text.PrintSeparator();
                        if (!string.IsNullOrEmpty(command.Object))
                        {
                            UseItem(command.Object);
                        }
                        else
                        {
                            Text.PrintWrappedText("Please specify the item you'd like to use.");
                        }
                        break;

                    case "give":
                        Console.Clear();
                        Text.PrintSeparator();
                        if (!string.IsNullOrEmpty(command.Object))
                        {
                            GiveItem(command.Object);
                        }
                        else
                        {
                            Text.PrintWrappedText("Please specify the item you'd like to give.");
                        }
                        break;

                    case "inventory":
                        Console.Clear();
                        Text.PrintSeparator();
                        ShowInventory();
                        break;

                    case "show map":
                        Console.Clear();
                        Text.PrintSeparator();
                        ShowMap(); // Call the ShowMap function here
                        break;

                    case "help":
                        Console.Clear();
                        Text.PrintSeparator();
                        ShowHelp();
                        break;

                    case "quit":
                        Console.Clear();
                        Text.PrintSeparator();
                        Text.center("Thank you for playing. Goodbye!", ConsoleColor.White);
                        playing = false;
                        break;
                    case "alupigus":
                        Console.Clear();
                        Text.PrintSeparator();
                        Text.center("AI spart sistemu", ConsoleColor.Green);
                        hasFinalItems = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
    }

    // Function to handle taking an item
    private void TakeItem(string itemName)
    {
        // Normalize the input to make it case-insensitive and trim any extra spaces
        string normalizedItemName = itemName.Trim().ToLower();

        // Find the item in the current room by normalizing the item names for comparison
        Item foundItem = CurrentRoom.Items.Find(item => item.Name.ToLower() == normalizedItemName);

        if (foundItem != null)
        {

            Inventory.Add(foundItem);
            CurrentRoom.Items.Remove(foundItem);
            Text.PrintWrappedText($"You picked up the {foundItem.Name}.", foundItem.Name, ConsoleColor.DarkMagenta);
            IncreaseScore(10); // Increase score for collecting item

            if (AsciiArt.ItemAsciiArt.TryGetValue(foundItem.Name.ToLower(), out string[] asciiArt))
            {
                Text.DisplayAsciiArt(asciiArt); // Display the ASCII art
            }
            else
            {
                Console.WriteLine("No ASCII art available for this item.");
            }

            if (foundItem.Name.ToLower() == "fishing net")
            {
                RiverUpstream.hasFishingNet = true; // Track that the player has the Fishing Net
            }
        }
        else
        {
            Text.PrintWrappedText("Item not found. Make sure you type the exact name of the item.");
        }
    }

    // Function to give an item to an NPC
    private void GiveItem(string itemName)
    {
        // Normalize the input to make it case-insensitive and trim any extra spaces
        string normalizedItemName = itemName.Trim().ToLower();

        // Check if the item exists in the player's inventory
        Item foundItem = Inventory.Find(item => item.Name.ToLower() == normalizedItemName);

        if (foundItem != null)
        {
            // Delegate the logic to the current room
            CurrentRoom.GiveItem(this, foundItem.Name);
        }
        else
        {
            Console.WriteLine($"You don't have a {itemName} in your inventory.");
        }

    }

    // Function to show inventory
    private void ShowInventory()
    {
        if (Inventory.Count > 0)
        {
            Console.Clear();
            Console.WriteLine("You have the following items in your inventory:");
            foreach (var item in Inventory)
            {
                Text.PrintWrappedText($"- {item.Name}: {item.Description}", item.Name, ConsoleColor.DarkMagenta);
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your inventory is empty.");
        }
    }

    // Function to use an item
    public void UseItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Console.WriteLine("Please specify the item you'd like to use.");
            return;
        }

        string normalizedItemName = itemName.Trim().ToLower();
        Item foundItem = Inventory.Find(item => item.Name.ToLower() == normalizedItemName);

        if (foundItem != null)
        {
            CurrentRoom.UseItem(this, normalizedItemName); // Delegate to the current room
        }
        else
        {
            Console.WriteLine("You don't have that item in your inventory.");
        }
    }

    // Move to the specified room
    private void MoveToRoom(string direction)
    {
        if (CurrentRoom.Name == "Leaking Pipelines" && direction == "west")
        {
            if (hasStorageKey)
            {
                Console.Clear();
                CurrentRoom = CurrentRoom.Exits[direction];
                CurrentRoom.Enter();


            }
            else if (!hasStorageKey)
            {
                Console.Clear();
                Text.PrintWrappedText("The door to Engineer's Tomas Workshop is locked. You need the Storage Key to enter.", "Engineer's Tomas Workshop", ConsoleColor.Blue);
                return;
            }
        }
        else if (CurrentRoom.Exits.ContainsKey(direction))
        {
            Console.Clear();
            CurrentRoom = CurrentRoom.Exits[direction];
            CurrentRoom.Enter();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You can't go that way.");
        }

    }

    private void ShowHelp()
    {

        Console.WriteLine("Available commands:");
        Text.PrintWrappedText("- explore: Look around the room for items or clues.", "explore", ConsoleColor.Gray);
        Text.PrintWrappedText("- take [item]: Pick up an item from the room.", "take", ConsoleColor.Gray);
        Text.PrintWrappedText("- go [direction]: Move to another room (e.g., 'go east').", "go", ConsoleColor.Gray);
        Text.PrintWrappedText("- talk: Speak to the NPC in the room.");
        Text.PrintWrappedText("- use [item]: Use an item from your inventory (e.g., 'use wrench').", "use", ConsoleColor.Gray);
        Text.PrintWrappedText("- give [item]: Give an item to the NPC in the room (e.g., 'give Hygiene Practice Leaflet').", "give", ConsoleColor.Gray);
        Text.PrintWrappedText("- inventory: View the items in your inventory.", "inventory", ConsoleColor.Gray);
        Text.PrintWrappedText("- show map: Show the village map (only if you have it).", "show map", ConsoleColor.Gray);
        Text.PrintWrappedText("- quit: Exit the game.", "quit", ConsoleColor.Gray);
        Text.PrintWrappedText("- help: Show available commands.", "help", ConsoleColor.Gray);
    }

    // Show map function
    public void ShowMap()
    {
        if (hasMap) // Check if the player has the map
        {

    Console.WriteLine("                                                      ╔══════╗   ");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                              ╔═══════╝      ╚═══════╗");
    Console.WriteLine("                                              ║       COMMUNITY      ║");
    Console.WriteLine("                                              ║    HEALTH  CENTER    ║");
    Console.WriteLine("                                              ╚═══════╗      ╔═══════╝");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                                      ║      ║   ");
    Console.WriteLine("                                                      ╚══════╝   ");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("                                                         │           ");
    Console.WriteLine("                                                       █ │ █               ");
    Console.WriteLine("                                                       █ │ █                 ");
    Console.WriteLine("                                                       █ │ █                             ");
    Console.WriteLine("                                                       █ │ █             ");
    Console.WriteLine("                                                       █ │ █          ");
    Console.WriteLine("                                                       █ │ █               ");
    Console.WriteLine("                                                       █ │ █                    ");
    Console.WriteLine("                                                         │                                  ");
    Console.WriteLine("          █   ~         █                                    ░ ░ ░                                 ▄▄▄▄     ");
    Console.WriteLine("          █          ~  █                                  ░░  ░░  ░                             ▓▓▓▓▓▓▓▓   ");
    Console.WriteLine("           ██     ~      █                                ░                                    ▓▓▓▓▓▓▓▓▓▓▓▓ ");
    Console.WriteLine("            █  ~          █                             ██                                    ══════════════ ");
    Console.WriteLine("            █          ~  █                           █▓▓▓▓▓█                                  │ │      │ │ ");
    Console.WriteLine("           ██ ~         ██                          █▓▓    ▓▓▓▓█                               │ │      │ │ ");
    Console.WriteLine("        ██       ~   ██     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄       █▓▓        ▓▓▓▓▓█       ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄      │ │      │ │ ");
    Console.WriteLine("      RIVER UPSTREAM       ─────────────────    █▓▓░  VILLAGE   ▓▓▓▓█    ─────────────────     │ │      │ │ ");
    Console.WriteLine("   ██    ~        ██        ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀    █▓░   TOWN HALL    ▓▓▓▓█   ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀    ┌▀▀▀▀▀▀▀▀▀▀▀▀▀▀┐");
    Console.WriteLine("  █       ~     ██                               █                 █                         ║──┼──┼──┼──┼──║");
    Console.WriteLine("██  ~        ██                                  ███████████████████                         ║   DRY WELL   ║");
    Console.WriteLine(" █      ~   █                                                                                ║──┼──┼──┼──┼──║ ");
    Console.WriteLine(" █   ~       █                                                                               ╚▄▄▄▄▄▄▄▄▄▄▄▄▄▄╝  ");
    Console.WriteLine("   █       ~   ██");
    Console.WriteLine("                                                         │           ");
    Console.WriteLine("                                                       █ │ █               ");
    Console.WriteLine("                                                       █ │ █                 ");
    Console.WriteLine("                                                       █ │ █                             ");
    Console.WriteLine("                                                       █ │ █             ");
    Console.WriteLine("                                                       █ │ █          ");
    Console.WriteLine("                                                       █ │ █               ");
    Console.WriteLine("                                                       █ │ █                    ");
    Console.WriteLine("                                                         │                                  ");
    Console.WriteLine("                                                                                                  ▒█░ ");
    Console.WriteLine("    ██████████                                   ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄                               ▒█░ ");
    Console.WriteLine("  ██▓▓▓▓▓▓░░  ██                                 █        ▓▒▒░░  ░█                              ░▓█▒░ ");
    Console.WriteLine("    ██████▓▓░░  ████                             █▒▒▒░    ▓▒▒░ ░ ░█                              ██▓██ ");
    Console.WriteLine("    ENGINEER TOMAS' WORKSHOP                     █ ▒▒░    ▓▒▒░░  ░█                              ██▓██ ");
    Console.WriteLine("          ██▓▓▓▓░░  ██         ▄▄▄▄▄▄▄▄▄▄▄▄▄▄    ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀    ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄      ██▓▓▓▓▓██ ");
    Console.WriteLine("          ██▓▓▓▓▓▓░░  ████    ────────────────     █  ░   ▓▒▒  ░█     ────────────────────     ██▓▒▒▒▓██  ");
    Console.WriteLine("        ██▒▒██▓▓▓▓▓▓░░░░  ██   ▀▀▀▀▀▀▀▀▀▀▀▀▀▀      █  ░   ▓▒▒  ░█      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀    ███▓▒▒▒▒▒▓██▓ ");
    Console.WriteLine("      ██▒▒▒▒▒▒████▓▓▓▓░░░░  ██                     LEAKING PIPES                             ███▓▒▒▒▒▒▓██▓ ");
    Console.WriteLine("    ██    ▒▒██    ██▓▓▓▓░░░░██                     █  ░   ▓▒▒  ░█                     RAINWATER HARVESTING SYSTEM");
    Console.WriteLine("  ██      ██      ██▓▓▓▓▓▓██                       █  ░   ▓▒▒  ░█                          ▒█▓▓▒▒▒▒░░░▒▒▓██░");
    Console.WriteLine("██      ██          ██▓▓██                         █  ░   ▓▒▒  ░█                        ░██▓▓▓▒▒░░░░░░░▒▒▓██");
    Console.WriteLine("                      ██                                                                 ░██▓  ░▒░░░░░░░▒▒▓██");
    Console.WriteLine("                                                         │                               ░██▓  ░▒░░░░░░░▒▒▓██ ");
    Console.WriteLine("                                                       █ │ █                               ▒█▓▒░░░░░▒▒▒▒▓██░ ");
    Console.WriteLine("                                                       █ │ █                               ░██▓█▓   ▒▒▓▓▓██░  ");
    Console.WriteLine("                                                       █ │ █                                     ░░░░        ");
    Console.WriteLine("                                                       █ │ █         ");
    Console.WriteLine("                                                       █ │ █               ");
    Console.WriteLine("                                                       █ │ █                 ");
    Console.WriteLine("                                                       █ │ █                    ");
    Console.WriteLine("                                                         │                                  ");
    Console.WriteLine("                                                                 ");
    Console.WriteLine("                                                   ▓▓▓▓▒▒▓▒▒▒▒▒▒░                          ");
    Console.WriteLine("                                                  ░░▒▒░░    ░░▒                            ");
    Console.WriteLine("                                                 ░▓▓▓▓█▒▓▓▒▒▒▒▒▒▒▒                         ");
    Console.WriteLine("                                                 ▓▓▓▓█▒▒▒▓▒▒▒▒▒█░▒░                         ");
    Console.WriteLine("                                                 ▓▓▓▓█▒▒▒▓▒▒░▒▒█░▒░                         ");
    Console.WriteLine("                                                 ▓▓▓▓█▒▒▒▓▒▒▒▒▒█░▒░                         ");
    Console.WriteLine("                                          CONTAMINATED WATER STORAGE TANKS                        ");
    Console.WriteLine("                                                 ▓▓▓▓█▒▒▒▓▒▒▒▒▒█▒▒░                         ");
    Console.WriteLine("                                                  ▓▓▓▓█▒▒▓▒░▒▒█▒░▒                           ");
    Console.WriteLine("                                                  ▒░▒▒░░      ░░░                            ");
    Console.WriteLine("                                                   ▓▓▓▓▒▒▓▒▒▒▒▒▒░                            ");
    Console.WriteLine("                                                    ░░░░░░░░░░░░                             ");

        }
        else
        {
            Console.Clear();
            Console.WriteLine("You don't have the map yet.");
        }
    }

    public void IncreaseScore(int points)
    {
        Score += points;
    }






    public void CheckFinalItems(Game game)
    {
        if (RainwaterHarvestingSystem.hasJarofRainwater && RiverUpstream.hasFish && DryWell.hasRustyBucket && game.hasPurifiedWaterCrystal)
        {
            Text.PrintWrappedText("You now have all the items that Mayor Anwar asked for. You can choose to end your quest here...or you can go around and talk to more people to gain knowladge about the problem that the village was facing before you intervened. ", "You now have all the items that Mayor Anwar asked for.", ConsoleColor.Green);
            game.hasFinalItems = true;
        }
    }

    public void DisplayBasicEnding()
    {
     Console.Clear();
    Text.PrintSeparator();
    Text.PrintWrappedText("Your journey in the village ends with mixed results.");
    Text.PrintWrappedText("The community continues to face significant challenges, and the shadows of water scarcity and pollution linger over their lives. Your efforts, though well-intentioned, did not create the lasting impact the village desperately needed.");
    Text.PrintWrappedText("As you reflect on your choices, you realize that meaningful change requires persistence, commitment, and the courage to tackle the hardest problems.");
    Text.PrintWrappedText("The village remains a stark reminder that the fight for clean water is far from over. Will you take this lesson and strive to do better in the future?");
    Text.PrintSeparator();
    Console.WriteLine("Press any key to reflect on your choices...");
    Console.ReadKey();
    Process.Start(new ProcessStartInfo
    {
        FileName = "taskkill",
        Arguments = "/F /IM cmd.exe",
        CreateNoWindow = true,
        UseShellExecute = false
    });
    Environment.Exit(0);
}
    public void DisplayStandardEnding()
    {
 Console.Clear();
    Text.PrintSeparator();
    Text.PrintWrappedText("Your journey in the village has brought about positive change.");
    Text.PrintWrappedText("The community is stronger, with better access to clean water and improved infrastructure. Health and livelihoods are steadily improving, and hope has returned to the hearts of the villagers.");
    Text.PrintWrappedText("However, the path to sustainability is long, and challenges still remain. Your efforts have laid a solid foundation, but the village will require continued dedication to thrive fully.");
    Text.PrintWrappedText("The villagers express their heartfelt gratitude for the progress you’ve made and hope that your actions inspire others to contribute to their future.");
    Text.PrintSeparator();
    Console.WriteLine("Press any key to celebrate your achievements...");
    Console.ReadKey();
    Environment.Exit(0);
    }
    public void DisplayPerfectEnding()
    {
Console.Clear();
    Text.PrintSeparator();
    Text.PrintWrappedText("Your work has transformed the village into a thriving, sustainable community.");
    Text.PrintWrappedText("Clean water flows freely, supporting health, agriculture, and daily life. The once-polluted river is now a vibrant habitat, and the villagers have embraced practices that ensure long-term water conservation.");
    Text.PrintWrappedText("The village stands as a model of resilience and sustainability, inspiring other communities to take action. The changes you’ve implemented will ripple through generations, proving that one person’s actions can make a monumental difference.");
    Text.PrintWrappedText("As you leave the village, you carry with you the knowledge that you’ve not just solved problems but created a legacy of hope and possibility.");
    Text.PrintSeparator();
    Console.WriteLine("Press any key to bask in your success...");
    Console.ReadKey();
    Environment.Exit(0);
    }

    public void ReceiveItem(Item item)
    {
        // Add the item to the inventory
        Inventory.Add(item);

        // Notify the player
        Text.PrintWrappedText($"You received the {item.Name}.", item.Name, ConsoleColor.DarkMagenta);

        // Check if ASCII art exists for the item and display it
        if (AsciiArt.ItemAsciiArt.TryGetValue(item.Name.ToLower(), out string[] asciiArt))
        {
            Text.DisplayAsciiArt(asciiArt); // Display ASCII art
        }
        else
        {
            Console.WriteLine("No ASCII art available for this item.");
        }
    }


}

