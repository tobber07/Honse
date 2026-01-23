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
        
        Screen.DisplayText(sprite, x, y);
    }

    void Draw()
    {
        string sprite = animation.GetNextSprite();
        
        Screen.DisplayText(sprite, x, y);
    }
}