using System;
using System.IO.Compression;
using System.Runtime.InteropServices;


namespace Honse;


public static class Program
{

    static void Main(string[] args)
    {
        Console.SetWindowSize(Screen.Width, Screen.Height);
        Console.CursorVisible = false;
        
        //StartScreen.Display();
        
        Manager.StartGame();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            
            Screen.Update();
            Manager.UpdateGame();
            
            Thread.Sleep(100);
        }
        
        Console.ReadLine();
    }

    static int LongestName(Horse[] horses)
    {
        int longestName = 0;
        foreach (Horse horse in horses)
        {
            longestName = int.Max(longestName, horse.GetName().Length);
        }

        return longestName;
    }

}
