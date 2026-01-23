using System.Numerics;
using System.Text.RegularExpressions;

namespace Honse;

public class Horse
{
    private int x;
    private int y;
    private string name;
    private Animation animation;


    public Horse(int x, int y, string name, Animation animation)
    {
        this.x = x;
        this.y = y;
        this.name = name;
        this.animation = animation;
    }
    
    
    public void MoveTo(int x, int y)
    {
        //clear current horse
        Clear();
        
        //move horse
        this.x = x;
        this.y = y;
        
        //draw horse at new pos
        Draw();
    }

    public void Move(int x, int y)
    {
        MoveTo(this.x + x, this.y + y);
    }

    void Clear()
    {
        string sprite = animation.GetSprite();
        //replaces every char in sprite with a blank space except \n
        sprite = Regex.Replace(sprite, @"[^\n]", " ");
        
        int lineNum = 0;
        foreach (string line in sprite.Split("\n"))
        {
            Console.SetCursorPosition(x, y+lineNum);
            Console.Write(line);
            lineNum++;
        }
    }

    void Draw()
    {
        string sprite = animation.GetNextSprite();
        
        int lineNum = 0;
        foreach (string line in sprite.Split("\n"))
        {
            Console.SetCursorPosition(x, y+lineNum);
            Console.Write(line);
            lineNum++;
        }
    }
}