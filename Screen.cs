namespace Honse;

public static class Screen
{
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
}