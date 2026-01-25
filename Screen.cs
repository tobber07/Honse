namespace Honse;

public static class Screen
{
    public const int Width = 200;
    public const int Height = 50;
    
    public static void DisplayText(string text, int x, int y)
    {
        int lineNum = 0;
        foreach (string line in text.Split("\n"))
        {
            Console.SetCursorPosition(x, y+lineNum);
            Console.Write(line);
            lineNum++;
        }
    }

    public static void Clear()
    {
        Console.Clear();
    }
}