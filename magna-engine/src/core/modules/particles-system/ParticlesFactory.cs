using Raylib_cs;

namespace Core.modules.particles_system;

public static class ParticlesFactory
{
    // public static ParticleEmitterData Basic()
    // {
    //     return new ParticleEmitterData
    //     {
    //         Interval = 0.01f,
    //         EmitCount = 10,
    //         AngleVariance = 180f,
    //     };
    // }
    //
    // public static ParticleEmitterData Water()
    // {
    //     return new ParticleEmitterData
    //     {
    //         ParticleData = new ParticleData
    //         {
    //             Lifespan = 2.0f, 
    //             ColorStart = new Color(75, 145, 255, 255),
    //             ColorEnd = new Color(0, 0, 255, 0),
    //             MinSizeStart = 6f,
    //             SizeEnd = 1f,
    //             Speed = 30.0f,
    //         },
    //         Angle = 180f,
    //         AngleVariance = 45.0f,
    //         LifespanMin = 1.0f,
    //         LifespanMax = 2.5f,
    //         SpeedMin = 1f,
    //         SpeedMax = 2f,
    //         Interval = 0.05f,
    //         EmitCount = 30
    //     };
    // }
    //
    // public static ParticleEmitterData Fire()
    // {
    //     return new ParticleEmitterData
    //     {
    //         ParticleData = new ParticleData
    //         {
    //             Lifespan = 2.0f,
    //             ColorStart = new Color(255, 120, 50, 255), // Orange
    //             ColorEnd = new Color(255, 0, 0, 0), // Red, fading to transparent
    //             MinSizeStart = 5f,
    //             SizeEnd = 1f,
    //             Speed = 40.0f,
    //         },
    //         Angle = 90f,
    //         AngleVariance = 20.0f,
    //         LifespanMin = 1.0f,
    //         LifespanMax = 2.5f,
    //         SpeedMin = 20f,
    //         SpeedMax = 50f,
    //         Interval = 0.04f,
    //         EmitCount = 10
    //     };
    // }
    //
    // public static ParticleEmitterData MagicSparkle()
    // {
    //     return new ParticleEmitterData
    //     {
    //         ParticleData = new ParticleData
    //         {
    //             Lifespan = 1.5f,
    //             ColorStart = new Color(255, 255, 0, 255), // Yellow
    //             ColorEnd = new Color(255, 255, 255, 0), // White, fading to transparent
    //             MinSizeStart = 3f,
    //             SizeEnd = 0.5f,
    //             Speed = 15.0f,
    //         },
    //         Angle = 0f,
    //         AngleVariance = 360.0f,
    //         LifespanMin = 2f,
    //         LifespanMax = 3f,
    //         SpeedMin = 10f,
    //         SpeedMax = 20f,
    //         Interval = 0.1f,
    //         EmitCount = 30
    //     };
    // }
    //
    // public static ParticleEmitterData Electricity()
    // {
    //     return new ParticleEmitterData
    //     {
    //         ParticleData = new ParticleData
    //         {
    //             Lifespan = 1.0f,
    //             ColorStart = new Color(125, 255, 200, 255), // Cyan
    //             ColorEnd = new Color(0, 75, 255, 0), // Blue
    //             MinSizeStart = 6f,
    //             SizeEnd = 3f,
    //             Speed = 60f,
    //         },
    //         AngleData =
    //         {
    //             Angle = 180f,
    //             AngleVariance = 50f,
    //         },
    //         
    //         LifeData =
    //         {
    //             LifespanMin = 1.5f,
    //             LifespanMax = 2f,
    //         },
    //         
    //         SpeedData =
    //         {
    //             SpeedMin = 30f,
    //             SpeedMax = 50f,
    //         },
    //         
    //         EmitData =
    //         {
    //             Interval = 0.4f,
    //             EmitCount = 20
    //         },
    //     };
    // }
    //
    public static ParticleData Snow()
    {
        return new ParticleData
        {
            Texture = Textures.GetSnowFlake(),
            
            RotationData =
            {
                MinRotationSpeed = -75,
                MaxRotationSpeed = 75,
            },
            
            WindData =
            {
                MinWindAmplitude = 2f,
                MaxWindAmplitude = 4f,
                MinWindFrequency = 0.5f,
                MaxWindFrequency = 1f,
            },
            
            NoiseData =
            {
                MaxNoise = 0.03f,
                MinNoise = 0.03f,   
            },
            
            SizeData =
            {
                SizeEnd = 4f,
                MinSizeStart = 5f,
                MaxSizeStart = 9f,
            },
            
            AngleData =
            {
                Angle = 180f,
                AngleVariance = 180f,
            },
            
            LifeData =
            {
                LifespanMin = 30f,
                LifespanMax = 38f,
            },
            
            ColorData =
            {
                ColorStart = new Color(255, 255, 255, 150),
                ColorEnd = new Color(255, 255, 255, 0),
            },
            
            SpeedData =
            {
                SpeedMin = 5,
                SpeedMax = 15,
            },
            
            EmitData =
            {
                Interval = 1.5f,
                EmitCount = 20,
            },
        };
    }
    
    // public static ParticleEmitterData GreenExplosion()
    // {
    //     return new ParticleEmitterData
    //     {
    //         Interval = 0.5f,
    //         EmitCount = 50,
    //         AngleVariance = 180f,
    //         LifespanMin = 3.5f,
    //         LifespanMax = 3.5f,
    //         ParticleData = new ParticleData
    //         {
    //             Speed = 20,
    //             ColorStart = Color.GREEN,
    //             ColorEnd = new Color(255, 255, 0, 0),
    //             MinSizeStart = 4,
    //             SizeEnd = 8,
    //         },
    //        
    //     };
    // }

    public static ParticleData SomeEffect()
    {
        return new ParticleData
        {
          ColorData =
          {
              ColorStart = Color.GREEN,
              ColorEnd = Color.YELLOW
          }
        };
    }
    
    public static ParticleData BloodHit()
    {
        return new ParticleData
        {
            PhysicsData =
            {
                Gravity = 3f,
            },
            
            NoiseData =
            {
                MinNoise = 10,
                MaxNoise = 10, 
            },
            
            SizeData =
            {
                MinSizeStart = 2.5f,
                MaxSizeStart = 3f,
                SizeEnd = 2f,
            },
            
            AngleData =
            {
                Angle = -90f,
                AngleVariance = 35,
            },
            
            LifeData =
            {
                LifespanMin = 0.5f,
                LifespanMax = 1.5f,
            },
            
            ColorData =
            {
                ColorStart = new Color(150, 0, 0, 255), 
                ColorEnd = new Color(150, 0, 0, 125),
            },
            
            SpeedData =
            {
                SpeedMin = 150,
                SpeedMax = 150,
            },
            
            EmitData =
            {
                Interval = 2f,
                EmitCount = 150
            },
        };
    }
}