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
                return new Command("show map", null); // Return a command specifically for showing the map
            }

            // For other commands, use the first word as the verb
            string verb = words[0];

            // Check if the verb is in the valid commands
            if (Array.Exists(validCommands, command => command == verb))
            {
                string secondWord = words.Length > 1 ? string.Join(" ", words[1..]) : null;  // Handle multi-word items like "Rusty Bucket"
                return new Command(verb, secondWord);
            }
            else
            {
                Console.WriteLine("I don't understand that command.");
                return null;
            }
        }
    }
}