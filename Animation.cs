namespace Honse;

public class Animation
{
    ///length of animation
    private int length;
    
    
    private int frame = 0;
    private string[] sprites;

    /// <param name="sprites">the path for the sprite of the animation</param>
    public Animation(string[] sprites)
    {
        length = sprites.Length;
        this.sprites = new string[length];
        for (int i = 0; i < length; i++)
        {
            this.sprites[i] = File.ReadAllText(sprites[i]);
        }
    }

    public string GetSprite()
    {
        return sprites[frame];
    }

    public string GetNextSprite()
    {
        NextSprite();
        return GetSprite();
    }
    
    public void NextSprite()
    {
        //increment frame
        frame = (frame + 1) % (length);
    }
}