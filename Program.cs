// See https://aka.ms/new-console-template for more information


namespace Honse;

public static class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        StartScreen.Display();
        Console.Beep(440, 1000);
        Console.Beep(880, 1000);
        Console.ReadKey();
        
        
        
        Horse horse = new Horse(10, 10, "peter", new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]));
        
        horse.MoveTo(50, 20);

        int pos = 40;
        int i = 0;
        while (true)
        {
            horse.MoveTo(pos, 20);
            Thread.Sleep(100);
            if (i > 0)
            {
                pos++;
                i = 0;
            }
            

            i++;
        }
        
        Console.ReadLine();
    }


}
