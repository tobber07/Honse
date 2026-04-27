namespace Honse;

public static class Manager
{
    private static Horse[] horses;
    private static long money = 1000;
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
            DisplayMoney();
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

            money -= horse.GetBet();
        }
        Screen.Clear();
        currentTrack.StartRace(horses);
    }

    public static void EndRace()
    {
        raceStarted = false;
        int winnings = CalculateWinnings(horses);
        money += winnings;
        Screen.DrawRect(Screen.GetCenterX() -9, 4,19,5, true, Screen.RectFillType.Hollow);
        Screen.DisplayTextCentered(winnings <= 0 ?  "Skill issue" : "Congratulations", 5);
        Screen.DisplayTextCentered("You won:", 6);
        Screen.DisplayTextCentered(winnings.ToString(), 7);

        Console.ReadKey();
        Screen.Clear();
        
        StartGame(); // start a new game
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

            DisplayMoney();
            int selectedHorseId = SelectHorse();
            
            //if space was pressed exit the loop to start the race
            if (selectedHorseId == -1)
            {
                if (GetTotalBets() > money)
                {
                    Screen.DrawRect(Screen.GetCenterX()-10, 13, 20, 6 , true, Screen.RectFillType.Hollow);
                    Screen.DisplayTextCentered("Not enough", 15);
                    Screen.DisplayTextCentered("money for bets", 16);
                    
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    break; // exit loop to start race
                }
            }
            Horse selectedHorse = horses[selectedHorseId];
            Screen.Clear();
            DisplayMoney();
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

    public static long GetTotalBets()
    {
        long total = 0;
        foreach (Horse horse in horses)
        {
            total += horse.GetBet();
        }

        return total;
    }

    private static void DisplayMoney()
    {
        Screen.DisplayText("Money: " + money, 2, 2);
        Screen.DisplayText("Bets: " + GetTotalBets(), 2,3);
    }
}