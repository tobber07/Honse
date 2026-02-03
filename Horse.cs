using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Honse;



struct HorseStats(float start, float middle, float end)
{
    public float start = start;
    public float middle = middle;
    public float end = end;
    
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
    private readonly HorseStats stats;
    private float distance;
    
    private float baseSpeed = 10;
    
    static Random rnd = new Random();

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

    public void MoveToX(int x)
    {
        MoveTo(x, this.y);
    }
    
    public void MoveToY(int y)
    {
        MoveTo(this.x, y);
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
        string blankName = Regex.Replace(name, @"[^\n]", " ");
        
        Screen.DisplayText(blankName, x, y-1);
        Screen.DisplayText(sprite, x, y);
    }

    /// <summary>
    /// Draws the horse at its current location
    /// </summary>
    void Draw()
    {
        string sprite = animation.GetNextSprite();
        
        Screen.DisplayText(name, x, y-1);
        Screen.DisplayText(sprite, x, y);
    }

    /// <summary>
    /// Returns a random name.
    /// </summary>
    /// <returns>Random name</returns>
    public static string RandomName()
    {
        string[] names = File.ReadAllLines("Assets/HorseNames.txt");
        return names[rnd.Next(0, names.Length - 1)];
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="track"></param>
    /// <returns>total distance</returns>
    public float RunAlongTrack(Track track)
    {
        RaceStage stage = track.GetRaceStage(distance);
        float speed = baseSpeed;
        switch (stage)
        {
            case RaceStage.Start:
                speed += stats.start;
                break;
            case RaceStage.Middle:
                speed += stats.middle;
                break;
            case RaceStage.End:
                speed += stats.end;
                break;
            case RaceStage.Finished:
                speed = 0;
                break;
        }
        
        distance += speed;
        distance = float.Min(distance, track.GetLength());
        return distance;
    }

    public float GetDistance()
    {
        return distance;
    }
    
    
}