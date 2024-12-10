namespace TextBasedGame
{
    // Parser class to handle player input
    public class Parser
    {
        private static readonly string[] validCommands = { "go", "take", "explore", "help", "quit", "talk", "use", "inventory", "show map", "give", "alupigus" };

        public Command ParseCommand(string input)
        {
            input = input.ToLower().Trim(); // Normalize input
            string[] words = input.Split(' ');

            // Check if the input is exactly "show map"
            if (input == "show map")
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                return new Command("show map", null); // Return a command specifically for showing the map
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }

            // For other commands, use the first word as the verb
            string verb = words[0];

            // Check if the verb is in the valid commands
            if (Array.Exists(validCommands, command => command == verb))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string secondWord = words.Length > 1 ? string.Join(" ", words[1..]) : null;  // Handle multi-word items like "Rusty Bucket"
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                return new Command(verb, secondWord);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            else
            {
                Console.WriteLine("I don't understand that command.");
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
    }
}