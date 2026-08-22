using System.Diagnostics;

namespace Honse;

public enum RaceStage
{
    Start,
    Middle,
    End,
    Finished
}

public class Track
{
    private Horse[] horses;

    private int tracks; // the amount of tracks, 1 horse runs on 1 track
    
    private float length = 1400f; //the total length of the track, standard is 1400m
    private float startDistance = 400; //the point the track goes from start to middle
    private float middleDistance = 1000; //the point the track goes from middle to end

    private int AmountFinished = 0;
    
        
    public Track(float length, float startDistance, float middleDistance)
    {
        this.length = length;
        this.startDistance = startDistance;
        this.middleDistance = middleDistance;
    }
    
    public void StartRace(Horse[] horses)
    {
        this.horses = horses;
        tracks = horses.Length;
        
        for (int i = 0; i < tracks; i++)
        {
            horses[i].SetPosition(Screen.Width / 2, 11 + (i * 3));
        }
        
    }

    public void EndRace()
    {
        Manager.EndRace();   
    }

    public void Update()
    {
        float furthestDistance = 0;
        //moves horses and finds the furthest
        for (int i = 0; i < horses.Length; i++)
        {
            Horse horse = horses[i];
            
            float distance = horse.RunAlongTrack(this);
            if (distance > furthestDistance) furthestDistance = distance;
            
            if (horse.finishPosition != 0) continue;
            
            //crossed the finish line
            if (distance >= length)
            {
                AmountFinished++;
                horse.finishPosition = AmountFinished;
                Screen.DisplayText(AmountFinished.ToString(), Screen.GetCenterX() + 7, 11 + (i*3));

                if (AmountFinished >= horses.Length)
                {
                    EndRace();
                    return;
                }
            }
        }
        
        //draws the track
        this.Draw(furthestDistance);
        
        //moves the horses on the screen
        foreach (Horse horse in horses)
        {
            horse.MoveToX(Screen.GetCenterX()-(int)(furthestDistance-horse.GetDistance()));
        }
        
    }

    private void Draw(float distance)
    {
        string text = GenerateSingleTrack(distance);
        
        for (int i = 0; i < tracks+1; i++)
        {
            Screen.DisplayText(text, 0, 10 + (i*3));
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance">distance at the center of the screen</param>
    /// <returns></returns>
    public string GenerateSingleTrack(float distance)
    {
        string text = new string('=', Screen.Width);
        int offset = (int)(distance % 100);
        
        int amount = (int)float.Ceiling((Screen.Width-offset)/100.0f);

        int startValue = (int)distance - (Screen.Width / 2) + (100-offset);
        
        for (int i = 0; i < amount; i++)
        {
            text = text.Insert((100-offset) + (i * 100), (startValue + (i * 100)) + "m");
        }
        
        return text;
    }

    public RaceStage GetRaceStage(float distance)
    {
        if (distance < startDistance) return RaceStage.Start;
        if (distance < middleDistance) return RaceStage.Middle;
        if (distance < length) return RaceStage.End;
        return RaceStage.Finished;
    }

    public float GetLength()
    {
        return length;
    }
}