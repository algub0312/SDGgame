public class Text
{

    public static void PrintWithColor(string text, ConsoleColor color)//Formatting the text color
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void PrintSeparator()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 60));
        Console.WriteLine();
    }
    // Helper method to print a line with one colored word
    public static void PrintWithColoredWord(string before, string word, string after, ConsoleColor color)
    {
        Console.Write(before);                  // Print the text before the word
        Console.ForegroundColor = color;        // Set color for the word
        Console.Write(word);                    // Print the word in color
        Console.ResetColor();                   // Reset color back to default
        Console.WriteLine(after);               // Print the text after the word
    }
    public static void PrintWrappedText(string text, string wordToColor = null, ConsoleColor? color = null, int lineWidth = 60)
    {
        int currentPosition = 0;

        while (currentPosition < text.Length)
        {
            // Find the line end position, respecting the line width
            int lineEnd = Math.Min(currentPosition + lineWidth, text.Length);

            // Adjust line end if it cuts off a word
            if (lineEnd < text.Length && !char.IsWhiteSpace(text[lineEnd]))
            {
                lineEnd = text.LastIndexOf(' ', lineEnd);
            }

            // Get the substring for the current line
            string line = text.Substring(currentPosition, lineEnd - currentPosition).Trim();

            // If a word to color is specified and the line contains it
            if (!string.IsNullOrEmpty(wordToColor) && line.Contains(wordToColor) && color.HasValue)
            {
                // Split the line into parts: before, the target word, and after
                int wordIndex = line.IndexOf(wordToColor);
                string before = line.Substring(0, wordIndex);
                string word = line.Substring(wordIndex, wordToColor.Length);
                string after = line.Substring(wordIndex + wordToColor.Length);

                // Print each part with the target word in color
                Console.Write(before);
                Console.ForegroundColor = color.Value;  // Set the color for the target word
                Console.Write(word);
                Console.ResetColor();                   // Reset to default color
                Console.WriteLine(after);
            }

            else
            {
                // If no coloring is specified or the word isn't in the line, print it as is
                Console.WriteLine(line);
            }

            // Move to the next position
            currentPosition = lineEnd + 1;
        }
    }
    public void PrintPaddedText(string text, int paddingLines = 1)
    {
        for (int i = 0; i < paddingLines; i++)
        {
            Console.WriteLine();  // Print blank lines for padding
        }
        PrintWrappedText(text);
        for (int i = 0; i < paddingLines; i++)
        {
            Console.WriteLine();  // Print blank lines for padding
        }
    }
    public static void center(string message, ConsoleColor color, int lineWidth = 60)
    {
        int screenWidth = Console.WindowWidth;
        int stringWidth = message.Length;
        int spaces = (screenWidth / 2) + (stringWidth / 2);

        Console.ForegroundColor = color;
        Console.WriteLine(message.PadLeft(spaces));
        Console.ResetColor();

    }

    public static void DisplayAsciiArt(string[] asciiArt)
    {

        Console.WriteLine();
        // Display each line of ASCII art
        foreach (string line in asciiArt)
        {
            // Set cursor position to the calculated column

            Console.WriteLine(line);
        }
    }
}