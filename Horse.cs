using System.Numerics;

namespace Honse;

public class Horse
{
    private int x;
    private int y;
    private string name;
    private Animation animation;

    void MoveTo(int x, int y)
    {
        Clear();
        this.x = x;
        this.y = y;
        Draw();
    }

    void Clear()
    {
        
    }

    void Draw()
    {
        
    }
}