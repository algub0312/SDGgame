public class Room
{
    public string Name { get; set; }
    public string Description { get; set; }
    public NPC RoomNPC { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public Dictionary<string, Room> Exits { get; set; } = new Dictionary<string, Room>();

    public Room(string name, string description, NPC npc = null)
    {
        Name = name;
        Description = description;
        RoomNPC = npc;
    }

    public void Enter()
    {
        Text.PrintSeparator();
        // Display the room name and description
        Text.PrintWrappedText($"\nYou enter the {Name}.", Name, ConsoleColor.Blue);
        // Display available exits
        if (Exits.Count > 0)
        {
            string exits = string.Join(", ", Exits.Keys);
            Text.PrintWrappedText($"(exits: {exits})");
        }
        else
        {
            Text.PrintWrappedText("There are no visible exits from here.");
        }
        Text.PrintWrappedText(Description);

        // Display the NPC if one is present
        if (RoomNPC == null)
        {
            Console.WriteLine("There is no one here.");
        }
        else
        {
            Text.PrintWrappedText($"There is an NPC here: {RoomNPC.Name}. You can talk to them using the 'talk' command.", RoomNPC.Name, ConsoleColor.Yellow);
        }
    }

    public void ShowItems()
    {
        if (Items.Count > 0)
        {
            Console.WriteLine("Items available in the room:");
            foreach (var item in Items)
            {
                Text.PrintWrappedText($"- {item.Name}: {item.Description}", item.Name, ConsoleColor.DarkMagenta);
            }
        }
        else
        {
            Console.WriteLine("There are no items in this room.");
        }
    }
    public virtual void UseItem(Game game, string itemName)
    {
        Console.WriteLine($"You can't use {itemName} in the {Name}.");
    }
    public virtual void GiveItem(Game game, string itemName)
    {
        Console.WriteLine($"You cannot give {itemName} here.");
    }
    public virtual void TalkToNPC(Game game)
    {
        if (RoomNPC != null)
        {
            RoomNPC.ComplexDialogue(game); // Default NPC dialogue handling
        }
        else
        {
            Console.WriteLine("There's no one here to talk to.");
        }
    }
}