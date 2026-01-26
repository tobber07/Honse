using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Honse;



struct HorseStats(float start, float middle, float end)
{
    private float start = start;
    private float middle = middle;
    private float end = end;
    
    /// <summary>
    /// Generates random stats for a horse
    /// </summary>
    public static HorseStats Random()
    {
        Random rand = new Random();
        return new HorseStats(rand.NextSingle(), rand.NextSingle(), rand.NextSingle());
    }
}


public class Horse
{
    private int x;
    private int y;
    private string name;
    private readonly Animation animation;
    private HorseStats stats;


    public Horse(int x, int y, string name, Animation animation)
    {
        this.x = x;
        this.y = y;
        this.name = name;
        this.animation = animation;
        stats = HorseStats.Random();
    }
    
    /// <summary>
    /// Removes current horse and shows a new one at new <c>x</c> and <c>y</c>
    /// </summary>
    /// <param name="x">New x</param>
    /// <param name="y">New y</param>
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

    
    /// <summary>
    /// Moves horse by <c>x</c> and <c>y</c> <br/>
    /// See also <see cref="MoveTo"/>
    /// </summary>
    /// <param name="x">Delta x</param>
    /// <param name="y">Delta y</param>
    public void Move(int x, int y)
    {
        MoveTo(this.x + x, this.y + y);
    }

    
    /// <summary>
    /// Creates a blank space the size of the horse at the location of the horse
    /// </summary>
    void Clear()
    {
        string sprite = animation.GetSprite();
        
        //replaces every char in sprite with a blank space except \n
        sprite = Regex.Replace(sprite, @"[^\n]", " "); // denne linje er udarbejded med hjælp fra AI
        
        Screen.DisplayText(sprite, x, y);
    }

    /// <summary>
    /// Draws the horse at its current location
    /// </summary>
    void Draw()
    {
        string sprite = animation.GetNextSprite();
        
        Screen.DisplayText(sprite, x, y);
    }
}