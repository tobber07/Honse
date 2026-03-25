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
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return;
        }
        
        int lineNum = 0;
        //splits the text at \n (new line) then loops through the lines
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
    
    public static void DisplayTextCentered(string[] text, int y)
    {
        int longest = 0;
        foreach (string line in text)
        {
            int len = line.Length;
            if (len > longest) longest = len;
        }
        foreach (string line in text)
        {
            DisplayText(line, (Screen.Width/2)-(longest/2), y);
        }
        
    }

    public static void DisplayTextWithFont(string text, string font, int x, int y)
    {
        foreach (char chr in text)
        {
            string newText = File.ReadAllText("Assets\\Fonts\\" + font + "\\" + chr + ".txt");
            Console.WriteLine("⢺ \n⠼⠄");
            DisplayText(newText, x, y);
        }
    }

    /// <summary>
    /// Clears the screen.
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
        Console.CursorVisible = false;
    }

    
    
    static int lastBufferWidth;
    static int lastBufferHeight;
    
    public static void Update()
    {
        int bufferWidth = Console.BufferWidth;
        int bufferHeight = Console.BufferHeight;
        
        if (bufferWidth < Width)
        {
            Console.SetBufferSize(Width, bufferHeight);
            Console.SetWindowSize(Width, bufferHeight);
        }

        if (bufferHeight < Height)
        {
            Console.SetBufferSize(bufferWidth, Height);
            Console.SetWindowSize(bufferWidth, Console.WindowHeight);
        }

        if (lastBufferWidth != bufferWidth || lastBufferHeight != bufferHeight)
        {
            Screen.Clear();
        }
        
        lastBufferWidth = bufferWidth;
        lastBufferHeight = bufferHeight;
    }
}