using Core;
using Raylib_cs;

namespace HelloWorld;

public class TestCircularBufferScene : Scene
{
    private int bufferSize = 10;
    private int[] circularBuffer;
    private int writeIndex = 0;
    
    public TestCircularBufferScene(string name) : base(name)
    {
        circularBuffer = new int[bufferSize];
        for (int i = 0; i < bufferSize; i++)
        {
            circularBuffer[i] = i + 1;
        }
    }

    protected internal override void Update(float dt)
    {
        base.Update(dt);
        int newData = GetNewData();
        circularBuffer[writeIndex] = newData;
        writeIndex = (writeIndex + 1) % bufferSize; // Move the write index in a circular manner

    }

    protected internal override void Draw()
    {
        base.Draw();
        DrawCircularBuffer();
    }
    
    private int GetNewData()
    {
        // Simulate getting new data from some source (e.g., sensor, network)
        return (int)(DateTime.Now.Ticks % 100); // Use the current time as dummy data
    }
    
    private void DrawCircularBuffer()
    {
        int lineHeight = Raylib.GetScreenHeight() / bufferSize;
        for (int i = 0; i < bufferSize; i++)
        {
            int data = circularBuffer[(writeIndex + i) % bufferSize];
            Raylib.DrawText($"Data {i + 1}: {data}", 10, i * lineHeight + 10, 20, Color.DARKGRAY);
        }
    }
}