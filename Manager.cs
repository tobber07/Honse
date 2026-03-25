namespace Honse;

public static class Manager
{
    private static Horse[] horses;
    private static long money;
    private static Track currentTrack;
    private static bool raceStarted = false;

    public static void StartGame()
    {
        currentTrack = new Track(400, 100, 3000);
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
        currentTrack.StartRace(horses);
    }

    public static void EndRace()
    {
        raceStarted = false;
        money += CalculateWinnings(horses);
    }

    public static int CalculateWinnings(Horse[] horses)
    {
        int totalHorses = horses.Length;
        int winnings = 0;
        foreach (Horse horse in horses)
        {
            horse.CalculateWinnings(totalHorses);
        }
        return winnings;
    }
    
    public static void SetBets()
    {
        while (true)
        {
            Horse selectedHorse = horses[SelectHorse()];
            Screen.Clear();
            Screen.DisplayTextCentered("Enter a bet for " + selectedHorse.GetName() + ":", 10);
            Console.SetCursorPosition(Screen.Width/2, 11);
            Console.ReadLine();
            break;
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
            }
            Screen.DisplayText(">", drawPosition-1, 11+selection);
        }

        return selection;
    }
}