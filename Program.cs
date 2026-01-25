using System;
using System.Runtime.InteropServices;


namespace Honse;


public static class Program
{

    static void Main(string[] args)
    {
        Console.SetWindowSize(Screen.Width, Screen.Height);
        Console.CursorVisible = false;
        
        // Console.SetBufferSize(Screen.Width, Screen.Height);


        Console.WriteLine(Console.WindowWidth);
        Console.WriteLine(Console.WindowHeight);
        
        // MusicPlayer.Play();
        // StartScreen.Display();
        Console.ReadKey();
        
        Console.WriteLine(Console.BufferWidth);
        Console.WriteLine(Console.BufferHeight);
        
        
        
        Horse horse = new Horse(10, 10, "peter", new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]));
        
        Horse horse2 = new Horse(10, 10, "peter", new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]));
        
        Horse horse3 = new Horse(10, 10, "peter", new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]));
        
        horse.MoveTo(50, 10);
        horse2.MoveTo(50, 15);
        horse3.MoveTo(50, 20);
        
        while (true)
        {
            horse.Move(1, 0);
            horse2.Move(1,0);
            horse3.Move(1,0);
            Thread.Sleep(100);
        }
        
        Console.ReadLine();
    }


}
