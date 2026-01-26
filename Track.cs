namespace Honse;

public class Track
{
    private Horse[] horses;
        
    Track(int amount)
    {
        horses = new Horse[amount];
        Animation animation = new Animation([
            "Assets/Horses/Horse1/run1.txt",
            "Assets/Horses/Horse1/run2.txt",
            "Assets/Horses/Horse1/run3.txt"
        ]);
        
        for (int i = 0; i < amount; i++)
        {
            horses[i] = new Horse(0, i * 5, Horse.RandomName(), animation);
        }
    }

    public void Update()
    {
        foreach (Horse horse in horses)
        {
            
        }
    }
}