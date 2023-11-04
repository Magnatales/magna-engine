namespace Motion;

public class Animation
{
    public readonly int StartingFrame;
    public readonly int EndingFrame;

    public Animation(int startingFrame, int endingFrame)
    {
        StartingFrame = startingFrame;
        EndingFrame = endingFrame;
    }
    
    public int Length()
    {
        return EndingFrame - StartingFrame + 1;
    }
}