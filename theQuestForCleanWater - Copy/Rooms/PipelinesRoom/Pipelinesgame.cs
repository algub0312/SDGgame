using System;

public class ClearCloggedPipes
{
    private char[,] grid = {
        { '|', '-', '|', '+' },
        { '+', '|', '-', '-' },
        { '+', '|', '|', '+' }
    };

    private char[,] solvedGrid = {
        { '-', '-', '-', '+' },
        { '+', '-', '-', '|' },
        { '+', '-', '-', '+' }
    };

    private int gridRows = 3;
    private int gridCols = 4;

    public bool Start()
    {
        Console.WriteLine("Rotate the pipes to connect from the top-left to the bottom-right.");

        bool puzzleSolved = false;

        while (!puzzleSolved)
        {
            DisplayGrid();
            Console.WriteLine("Enter your move in the format 'rotate row col' (e.g., 'rotate 0 1'):");
            string? input = Console.ReadLine()?.ToLower();

#pragma warning disable CS8604 // Possible null reference argument.
            if (ParseCommand(input, out int row, out int col))
            {
                RotatePipe(row, col);
                DisplayGrid();
                puzzleSolved = CheckPuzzleSolved();

                if (puzzleSolved)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You've cleared the clogged pipes and completed the connection!");
                    Console.ResetColor();
                    return true;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid command. Use format 'rotate row col'.");
                Console.ResetColor();
            }
#pragma warning restore CS8604 // Possible null reference argument.
        }
        return false;
    }

    private void RotatePipe(int row, int col)
    {
        char currentPipe = grid[row, col];
        char rotatedPipe;

        switch (currentPipe)
        {
            case '|': rotatedPipe = '-'; break;  // Vertical to horizontal
            case '-': rotatedPipe = '|'; break;  // Horizontal to vertical
            case '+': rotatedPipe = '+'; break;  // Plus stays the same
            default: rotatedPipe = currentPipe; break;
        }

        grid[row, col] = rotatedPipe;
        Console.WriteLine($"Rotated pipe at ({row}, {col}) to '{rotatedPipe}'");
    }

    private void DisplayGrid()
    {
        Console.Clear();
        Console.WriteLine("Current Pipe Layout:");
        Console.WriteLine("Hint: You have to connect from the point (0,0) to (1,2).");
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private bool ParseCommand(string input, out int row, out int col)
    {
        row = -1;
        col = -1;

        string[] parts = input.Split(' ');
        if (parts.Length == 3 && parts[0] == "rotate" &&
            int.TryParse(parts[1], out row) && int.TryParse(parts[2], out col))
        {
            return row >= 0 && row < gridRows && col >= 0 && col < gridCols;
        }
        return false;
    }

    private bool CheckPuzzleSolved()
    {
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
            {
                if (grid[i, j] != solvedGrid[i, j])
                {
                    return false; // Not solved if any pipe doesn't match
                }
            }
        }
        return true;
    }
}
