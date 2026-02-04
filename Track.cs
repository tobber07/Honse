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
    
    private float length = 1400; //the total length of the track, standard is 1400m
    private float startDistance = 400; //the point the track goes from start to middle
    private float middleDistance = 1000; //the point the track goes from middle to end

    private int AmountFinished = 0;
    
    public Track(int amount)
    {
        tracks = amount;
        
        SpawnHorses(amount);
    }
        
    public Track(int amount, float length, float startDistance, float middleDistance)
    {
        tracks = amount;
        this.length = length;
        this.startDistance = startDistance;
        this.middleDistance = middleDistance;
        
        SpawnHorses(amount);

        
    }

    void SpawnHorses(int amount)
    {
        horses = new Horse[amount];
        
        for (int i = 0; i < amount; i++)
        {
            Animation animation = new Animation([
                "Assets/Horses/Horse1/run1.txt",
                "Assets/Horses/Horse1/run2.txt",
                "Assets/Horses/Horse1/run3.txt"
            ]);
            horses[i] = new Horse(0, i * 5, Horse.RandomName(), animation);
            horses[i].MoveTo(Screen.Width / 2, 11 + (i * 3));
        }
    }

    public void Update()
    {
        float furthestDistance = 0;
        //moves horses and finds the furthest
        for (int i = 0; i < horses.Length; i++)
        {
            float distance = horses[i].RunAlongTrack(this);
            if (distance > furthestDistance) furthestDistance = distance;

            //crossed the finish line
            if (distance >= length)
            {
                
            }
            
        }
        
        //draws the track
        this.Draw(furthestDistance);
        
        //moves the horses on the screen
        foreach (Horse horse in horses)
        {
            horse.MoveToX(Screen.Width/2-(int)(furthestDistance-horse.GetDistance()));
        }
        
    }

    private void Draw(float distance)
    {
        string text = GenerateSingleTrack(distance);
        
        for (int i = 0; i < tracks; i++)
        {
            Screen.DisplayText(text, 0, 10 + (i*3));
            //horses[i].MoveTo(Screen.Width/2, 11 + (i*3));
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