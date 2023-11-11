using Raylib_cs;

namespace Core.modules.particles_system;

public struct ParticleData
{
    public Texture2D Texture = Textures.GetParticle();
    public ColorData ColorData = new();
    public AngleData AngleData = new();
    public LifeData LifeData = new();
    public SpeedData SpeedData = new();
    public EmitData EmitData = new();
    public SizeData SizeData = new();
    public NoiseData NoiseData = new();
    public WindData WindData = new();
    public PhysicsData PhysicsData = new();
    public RotationData RotationData = new();

    public ParticleData() { }
}

public struct RotationData
{
    public float MinRotationSpeed = 0f;
    public float MaxRotationSpeed = 0f;
    public RotationData() { }
}

public struct PhysicsData
{
    public float Gravity = 0f;
    public float Friction = 3f;
    public PhysicsData() { }
}

public struct WindData
{
    public float MinWindFrequency = 0f;
    public float MaxWindFrequency = 0f;
    public float MinWindAmplitude = 0f;
    public float MaxWindAmplitude = 0f;
    public WindData() { }
}


public struct SizeData
{
    public float MinSizeStart = 3f;
    public float MaxSizeStart = 3f;
    public float SizeEnd = 6f;
    public SizeData() { }
}

public struct AngleData
{
    public float Angle = 0f;
    public float AngleVariance = 45f;
    public AngleData() { }
}

public struct LifeData
{
    public float LifespanMin = 0.3f;
    public float LifespanMax = 0.8f;
    public LifeData() { }
}

public struct SpeedData
{
    public float SpeedMin = 10f;
    public float SpeedMax = 100f;
    public SpeedData() { }
}

public struct EmitData
{
    public float Interval = 0.1f;
    public int EmitCount = 30;
    public EmitData() { }
}

public struct ColorData
{
    public Color ColorStart = Color.YELLOW;
    public Color ColorEnd = Color.RED;
    public ColorData() { }
}

public struct NoiseData
{
    public float MinNoise = 0f;
    public float MaxNoise = 0f;
    public NoiseData() { }
}
