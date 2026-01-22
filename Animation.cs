namespace Honse;

class Animation
{
    ///length of animation
    private int length;
    
    
    private int frame = 0;
    private string[] frames;

    /// <param name="frames">the path for the frames of the animation</param>
    Animation(string[] frames)
    {
        this.frames = new string[frames.Length];
        for (int i = 0; i < frames.Length; i++)
        {
            this.frames[i] = File.ReadAllText(frames[i]);
        }
    }
}