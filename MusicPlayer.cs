namespace Honse;

using System.Media;

public static class MusicPlayer
{
    public static void Play()
    {
        SoundPlayer soundPlayer = new SoundPlayer(@"Assets/Music/mm.mp3"); 
        soundPlayer.Play();
    }
    
}