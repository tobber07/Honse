namespace Honse;

public class Animation
{
    ///length of animation
    private int length;
    
    //the current frame of the animation
    private int frame = 0;
    
    //array of all sprites in the animation
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

    /// <summary>
    /// Returns the sprite of the current frame
    /// </summary>
    /// <returns>Currently selected sprite</returns>
    public string GetSprite()
    {
        return sprites[frame];
    }

    /// <summary>
    /// Selects the next frame in the animation, then returns that frame.
    /// see also:
    /// <see cref="NextSprite"/>,
    /// <see cref="GetSprite"/>
    /// </summary>
    /// <returns>Next sprite in animation</returns>
    public string GetNextSprite()
    {
        NextSprite();
        return GetSprite();
    }
    
    /// <summary>
    /// Selects the next frame in the animation, if the end is reached go to frame 0.
    /// </summary>
    public void NextSprite()
    {
        //increment frame
        frame = (frame + 1) % (length);
    }
}