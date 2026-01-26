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
    /// Clears the screen.
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
    }
}