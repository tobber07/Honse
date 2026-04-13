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
    private int bet;

    private float mood;
    
    
    private float baseSpeed = 5f;
    private float statMultiplier = .5f;
    private float moodMultiplier = .25f;
    
    static Random rnd = new Random();

    //public bool finished = false;
    public int finishPosition = 0;

    public Horse(int x, int y, string name, Animation animation)
    {
        this.x = x;
        this.y = y;
        this.name = name;
        this.animation = animation;
        stats = HorseStats.Random();
        SetRandomMood();

        //randomize starting sprite
        for (int i = 0; i < rnd.Next(2); i++)
        {
            this.animation.NextSprite();
        }
        
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
        SetPosition(x,y);
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
        //replaces every char in sprite with a blank space except \n
        string blankSprite = Regex.Replace(animation.GetSprite(), @"[^\n]", " "); // denne linje er udarbejded med hjælp fra AI
        string blankName = Regex.Replace(name, @"[^\n]", " ");
        
        Screen.DisplayText(blankName, x, y-1);
        Screen.DisplayText(blankSprite, x, y);
    }

    /// <summary>
    /// Draws the horse at its current location
    /// </summary>
    void Draw()
    {
        //if the horse has finished don't select next frame to stop animation
        string sprite = finishPosition != 0 ? animation.GetSprite() :animation.GetNextSprite();
        
        Screen.DisplayText(name + "==", x, y-1);
        Screen.DisplayText(sprite, x, y);
    }

    public void DisplayStats(int height)
    {
        //INFO//
        Screen.DisplayText("┌────────┐\n│  /\\___ │\n│ / O  º\\│\n│/  /¯¯¯¯│\n└────────┘",Screen.GetCenterX()-15, height+1); //image
        Screen.DisplayText("Name: " + name, Screen.GetCenterX()-15 + 11, height+2); //name
        Screen.DisplayText("Bet: " + bet, Screen.GetCenterX()-15 + 11, height+4); //bet
        
        //STATS//
        Screen.DisplayText("Start:  " + stats.start.ToString("F3"), Screen.GetCenterX()-14, height+6); //speed
        Screen.DisplayText("Middle: " + stats.middle.ToString("F3"), Screen.GetCenterX()-14, height+7); //speed
        Screen.DisplayText("End:    " + stats.end.ToString("F3"), Screen.GetCenterX()-14, height+8); //speed
        
        //BORDER//
        Screen.DrawRect(Screen.GetCenterX()-16, height, 16*2, 10); //border
        
        
        Screen.DisplayText("[Set bet]",Screen.GetCenterX()-15, height+10);
        Screen.DisplayText("[Back]",Screen.GetCenterX()+9, height+10);
        
        int selection = 0;
        while (true)
        {
            if (Console.KeyAvailable)
            {
                Screen.DisplayText(" ", Screen.GetCenterX()-16 + selection*24, height+10); //clear cursor
                ConsoleKey key = Console.ReadKey(true).Key;
                
                if (key == ConsoleKey.RightArrow)
                {
                    selection = int.Min(selection + 1, 1);
                }

                else if (key == ConsoleKey.LeftArrow)
                {
                    selection = int.Max(selection - 1, 0);
                }

                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            Screen.DisplayText(">", Screen.GetCenterX()-16 + selection*24, height+10); // draw cursor
        }

        if (selection == 0)
        {
            //set bet
            Screen.DisplayText("        ", Screen.GetCenterX()+1, height+4); // clear
            Console.SetCursorPosition(Screen.GetCenterX()+1, height+4);
            Console.CursorVisible = true;
            while (true)
            {
                String input = Console.ReadLine();
                int bet;
                if (int.TryParse(input, out bet))
                {
                    SetBet(bet);
                    break;
                }
                Screen.DisplayText("        ", Screen.GetCenterX()+1, height+4); //clear
                Console.SetCursorPosition(Screen.GetCenterX()+1, height+4);
            }
            
            Console.CursorVisible = false;
        }
        else if (selection == 1)
        {
            //go back
            return;
        }
        
    }

    public void SetRandomMood()
    {
        mood = rnd.NextSingle();
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
                speed += stats.start * statMultiplier + mood * moodMultiplier;
                break;
            case RaceStage.Middle:
                speed += stats.middle * statMultiplier + mood * moodMultiplier;
                break;
            case RaceStage.End:
                speed += stats.end * statMultiplier + mood * moodMultiplier;
                break;
            case RaceStage.Finished:
                speed = 0;
                break;
        }
        
        distance += speed;
        distance = float.Min(distance, track.GetLength());
        return distance;
    }

    public int CalculateWinnings(int totalHorses)
    {
        if (finishPosition <= totalHorses/2)
        {
            //1 << finishPosition is a substitution for 2^finishPosition
            return (int)(bet * ((float)totalHorses / (1 << finishPosition)));
        }
        return 0;
    }

    public float GetDistance()
    {
        return distance;
    }

    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Sets the position of the horse without updating the visuals;
    /// </summary>
    /// <param name="x">New x</param>
    /// <param name="y">Mew y</param>
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetBet(int bet)
    {
        this.bet = bet;
    }
    
}