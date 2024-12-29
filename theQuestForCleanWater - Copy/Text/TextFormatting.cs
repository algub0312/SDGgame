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
        Console.ForegroundColor = color;        // Set color for the words
        Console.Write(word);                    // Print the word in color
        Console.ResetColor();                   // Reset color back to default
        Console.WriteLine(after);               // Print the text after the word
    }
    public static void PrintWrappedText(string text, string? wordToColor = null, ConsoleColor? color = null, int lineWidth = 60, int speed = 20)
    {
        int currentPosition = 0;
        bool skipDelay = false;

        Thread keyListener = new Thread(() =>
        {
            while (!skipDelay) // Only monitor while skipDelay is false
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true); // Consume the key press
                    skipDelay = true;     // Set flag to skip delay
                }
            }
        })
        {
            IsBackground = true
        };
        keyListener.Start();

        while (currentPosition < text.Length)
        {
            int lineEnd = Math.Min(currentPosition + lineWidth, text.Length);

            if (lineEnd < text.Length && !char.IsWhiteSpace(text[lineEnd]))
            {
                lineEnd = text.LastIndexOf(' ', lineEnd);
            }

            string line = text.Substring(currentPosition, lineEnd - currentPosition).Trim();

            if (!string.IsNullOrEmpty(wordToColor) && line.Contains(wordToColor) && color.HasValue)
            {
                int wordIndex = line.IndexOf(wordToColor);
                string before = line.Substring(0, wordIndex);
                string word = line.Substring(wordIndex, wordToColor.Length);
                string after = line.Substring(wordIndex + wordToColor.Length);

                foreach (char c in before)
                {
                    Console.Write(c);
                    if (!skipDelay) Thread.Sleep(speed);
                }

                Console.ForegroundColor = color.Value;
                foreach (char c in word)
                {
                    Console.Write(c);
                    if (!skipDelay) Thread.Sleep(speed);
                }

                Console.ResetColor();
                foreach (char c in after)
                {
                    Console.Write(c);
                    if (!skipDelay) Thread.Sleep(speed);
                }
                Console.WriteLine();
            }
            else
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    if (!skipDelay) Thread.Sleep(speed);
                }
                Console.WriteLine();
            }

            currentPosition = lineEnd + 1;
        }

     
        skipDelay = true;
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
Console.WriteLine(line);
        }
    }


 public static void ShowTopRightMessage(string message, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;

            // Calculate the position for the top-right corner
            int topRightX = Console.WindowWidth - message.Length - 1;
            int topRightY = 0;

            // Display the message in the top-right corner
            Console.SetCursorPosition(topRightX, topRightY);
            Console.Write(message);

            Console.SetCursorPosition(originalLeft, originalTop);

            Thread.Sleep(100);
        }

        // Clear the message when the loop ends
        int clearX = Console.WindowWidth - message.Length - 1;
        Console.SetCursorPosition(clearX, 0);
        Console.Write(new string(' ', message.Length));
    }
    

}