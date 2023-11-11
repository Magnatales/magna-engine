// using System.Numerics;
// using Log;
// using Raylib_cs;
//
// namespace Core.modules.particles_system;
//
// public class SparkParticles : ParticleSystem
// {
//     private readonly IParticleEmitter _emitter;
//     public SparkParticles(IParticleEmitter emitter) : base(emitter.Position, Textures.GetParticle(), 2000)
//     {
//         _emitter = emitter;
//         minNumParticles = 2;
//         maxNumParticles = 4;
//     }
//
//     protected override void InitializeParticle(ref Particle p, Vector2 where)
//     {
//         var acceleration = Vector2.UnitY * 150;  // Adjust the acceleration
//         var scale = RaymathF.NextFloat(0.6f, 1.7f);  // Adjust the scale
//         var lifeTime = RaymathF.NextFloat(1.0f, 2.5f);  // Adjust the lifetime
//         var radius = RaymathF.NextFloat(5f, 20f);  // Adjust the radius
//         var angle = RaymathF.NextFloat(0, MathF.PI * 2);
//         var position = where;
//         var randomFireworkColor = new Color(
//             RaymathF.Next(200, 256),
//             RaymathF.Next(100, 256),
//             RaymathF.Next(0, 256),
//             255
//         );
//
//         var circularVelocity = RaymathF.NextFloat(50f, 200f);  // Adjust the circular velocity
//         var velocity = new Vector2(MathF.Cos(angle) * circularVelocity, MathF.Sin(angle) * circularVelocity);
//
//         p.Initialize(position, velocity, acceleration, randomFireworkColor, scale: scale, lifetime: lifeTime,
//             rotation: angle);
//     }
//
//     private float _timer;
//     private readonly float _totalTime = 0.1f;
//
//     public override void Update(float dt)
//     {
//         base.Update(dt);
//
//         _timer += dt;
//         if (_timer >= _totalTime)
//             AddParticles(_emitter.Position);
//         
//         //Logger.ScreenLog(count.ToString());
//     }
// }