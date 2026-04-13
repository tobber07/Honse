namespace Honse;

public static class Manager
{
    private static Horse[] horses;
    private static long money;
    private static Track currentTrack;
    private static bool raceStarted = false;

    public static void StartGame()
    {
        currentTrack = new Track(400, 100, 300);
        CreateHorses(12);
        SetBets();
    }

    public static void UpdateGame()
    {
        if (raceStarted)
        {
            currentTrack.Update(); 
        }

    }

    private static void CreateHorses(int amount)
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
            horses[i].SetPosition(Screen.Width / 2, 11 + (i * 3));
        }
    }

    public static void StartRace()
    {
        raceStarted = true;
        foreach (Horse horse in horses)
        {
            horse.SetRandomMood();
        }
        currentTrack.StartRace(horses);
    }

    public static void EndRace()
    {
        raceStarted = false;
        int winnings = CalculateWinnings(horses);
        money += winnings;
        Screen.DrawRect(Screen.Width/2 -9, 9,19,5, true, Screen.RectFillType.Hollow);
        Screen.DisplayTextCentered(winnings <= 0 ?  "Skill issue" : "Congratulations", 10);
        Screen.DisplayTextCentered("You won:", 11);
        Screen.DisplayTextCentered(winnings.ToString(), 12);
    }

    public static int CalculateWinnings(Horse[] horses)
    {
        int totalHorses = horses.Length;
        int winnings = 0;
        foreach (Horse horse in horses)
        {
            winnings += horse.CalculateWinnings(totalHorses);
            horse.SetBet(0);
        }
        return winnings;
    }
    
    public static void SetBets()
    {
        while (true)
        {
            Screen.Clear();
            
            Screen.DisplayTextCentered("PRESS SPACE TO START", 10 + horses.Length + 2);
            
            int selectedHorseId = SelectHorse();
            //if space was pressed exit the loop to start the race
            if (selectedHorseId == -1)
            {
                break;
            }
            Horse selectedHorse = horses[selectedHorseId];
            Screen.Clear();
            selectedHorse.DisplayStats(10);
        }
        
        StartRace();
    }
    
    
    public static int SelectHorse()
    {
        Screen.DisplayTextCentered("Select a horse:", 10);
        
        int longestName = 0;
        foreach (Horse horse in horses)
        {
            longestName = int.Max(longestName, horse.GetName().Length);
        }

        int drawPosition = Screen.Width/2 - (longestName+4)/2;
        
        int i = 1;
        foreach (Horse horse in horses)
        {
            Screen.DisplayText($"[{i}] {horse.GetName()}",drawPosition, 10+i);
            i++;
        }

        int selection = 0;
        while (true)
        {
            if (Console.KeyAvailable)
            {
                Screen.DisplayText(" ", drawPosition-1, 11+selection);
                ConsoleKey key = Console.ReadKey(true).Key;
                
                if (key == ConsoleKey.UpArrow)
                {
                    selection = int.Max(selection - 1, 0);
                }

                else if (key == ConsoleKey.DownArrow)
                {
                    selection = int.Min(selection + 1, horses.Length - 1);
                }

                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
                
                else if (key == ConsoleKey.Spacebar)
                {
                    selection = -1;
                    break;
                }
            }
            Screen.DisplayText(">", drawPosition-1, 11+selection);
        }

        return selection;
    }
}