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
        
        
        Track track = new Track(12);
        
        
        //track.SelectHorse();
        Console.ReadKey();
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
            track.Update();
            Thread.Sleep(100);
        }
        
        Console.ReadLine();
    }


}
