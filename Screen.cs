namespace Honse;

public static class Screen
{

    public enum RectFillType
    {
        Hollow,
        Full
    }
    
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

    
    public static void DrawRect(int x, int y, int width, int height, bool fill = false, RectFillType fillType = RectFillType.Hollow)
    {
        if(width <= 1 || height <= 1) return;
        
        Console.SetCursorPosition(x, y);
        Console.Write("╔");
        for (int i = 0; i < width-2; i++)
        {
            Console.Write("═");
        }
        Console.Write("╗");
        
        for (int i = y+1; i < y+height-1; i++)
        {
            Console.SetCursorPosition(x, i);
            Console.Write("║");
            
            Console.SetCursorPosition(x+width-1, i);
            Console.Write("║");
        }
        
        Console.SetCursorPosition(x, y+height-1);
        Console.Write("╚");
        for (int i = 0; i < width-2; i++)
        {
            Console.Write("═");
        }
        Console.Write("╝");

        if (!fill) return;
        //fills the rect
        for (int i = y + 1; i < y + height - 1; i++)
        {
            Console.SetCursorPosition(x+1, i);
            for (int j = 0; j < width - 2; j++)
            {
                switch (fillType)
                {
                    case RectFillType.Hollow:
                        Console.Write(" ");
                        break;
                    case RectFillType.Full:
                        Console.Write("█");
                        break;
                }
            }
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

    public static void ClearLine(int y)
    {
        Console.SetCursorPosition(0, y);
        for (int i = 0; i < Screen.Width; i++)
        {
            Console.Write(" ");
        }
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