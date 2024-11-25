public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Item(string name, ConsoleColor color, string description)
    {
        Name = name;
        Description = description;

    }
}