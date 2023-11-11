// using System.Numerics;
// using Raylib_cs;
//
// namespace Core.modules.particles_system;
//
// public class WaterParticles : ParticleSystem
// {
//     private readonly IParticleEmitter _emitter;
//
//     public WaterParticles(IParticleEmitter emitter) : base(emitter.Position, Textures.GetParticle(), 2000)
//     {
//         _emitter = emitter;
//         minNumParticles = 40;
//         maxNumParticles = 60;
//     }
//
//     protected override void InitializeParticle(ref Particle p, Vector2 where)
//     {
//         var acceleration = new Vector2(0, 100); // Gravity-like acceleration
//         var scale = RaymathF.NextFloat(0.5f, 1.2f); // Varying sizes
//         var lifeTime = RaymathF.NextFloat(3f, 5f); // Longer lifetimes
//         var radius = RaymathF.NextFloat(2f, 5f);
//         var angle = RaymathF.NextFloat(0, MathF.PI * 2);
//         var position = where + new Vector2(MathF.Cos(angle) * radius, MathF.Sin(angle) * radius);
//         var waterColor = new Color(
//             RaymathF.Next(20, 60),
//             RaymathF.Next(100, 200),
//             RaymathF.Next(225, 256),
//             225
//         );
//
//         var circularVelocity = RaymathF.NextFloat(5f, 20f); // Varying velocities
//         var velocity = new Vector2(MathF.Cos(angle) * circularVelocity, MathF.Sin(angle) * circularVelocity);
//
//         p.Initialize(position, velocity, acceleration, waterColor, scale: scale, lifetime: lifeTime, rotation: angle);
//     }
//
//
//     public static float ToRadians(float degrees)
//     {
//         return degrees * (float)(Math.PI / 180.0);
//     }
//
//     private float _timer;
//     private readonly float _totalTime = 1f;
//
//     public override void Update(float dt)
//     {
//         base.Update(dt);
//
//         _timer += dt;
//         if (_timer >= _totalTime)
//             //_timer = 0f;
//             AddParticles(_emitter.Position);
//     }
// }