namespace Honse;

public static class Screen
{
    public const int Width = 200;
    public const int Height = 50;
    
    /// <summary>
    /// Prints a string to any location in the console.
    /// </summary>
    /// <param name="text">The text to be displayed</param>
    /// <param name="x">Left</param>
    /// <param name="y">Top</param>
    /// <example>
    /// <code>
    ///     string text = """
    ///                     o   o
    ///                       -
    ///                   """
    ///     Screen.DisplayText(text, 10, 10)
    /// </code>
    /// </example>
    public static void DisplayText(string text, int x, int y)
    {
        int lineNum = 0;
        //splits the text at \n (new line) then loops through them
        foreach (string line in text.Split("\n"))
        {
            Console.SetCursorPosition(x, y+lineNum);
            Console.Write(line);
            lineNum++;
        }
    }

    /// <summary>
    /// Displays text centered horizontally
    /// </summary>
    /// <param name="text"></param>
    /// <param name="y"></param>
    public static void DisplayTextCentered(string text, int y)
    {
        int longest = 0;
        foreach (string line in text.Split("\n"))
        {
            int len = line.Length;
            if (len > longest) longest = len;
        }
        DisplayText(text, (Screen.Width/2)-(longest/2), y);
    }

    /// <summary>
    /// Clears the screen.
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
    }

    
    
    public static void Update()
    {
        if (Console.BufferWidth < Width)
        {
            Console.BufferWidth = Width;
        }

        if (Console.BufferHeight < Height)
        {
            Console.BufferHeight = Height;
        }
    }
}