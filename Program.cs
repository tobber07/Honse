// See https://aka.ms/new-console-template for more information


namespace Honse;

class Program
{
    static void Main(string[] args)
    {
        Horse horse = new Horse(10, 10, "peter", new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]));
        
        horse.MoveTo(50, 20);

        while (true)
        {
            horse.MoveTo(50, 20);
            Thread.Sleep(100);
        }
        
        Console.ReadLine();
    }
}
