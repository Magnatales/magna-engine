using System.Numerics;
using System.Security.Cryptography;
using Core;
using Raylib_cs;

namespace HelloWorld;

public class TestCollisionsScene : Scene
{
    public TestCollisionsScene(string name) : base(name)
    {
    }

    List<Sphere> spheres = new List<Sphere>();
    protected internal override void Update(float dt)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            Random random = new Random();
            Color randomColor = new Color((int)random.Next(256), (byte)random.Next(256), (byte)random.Next(256), 255);
            Sphere newSphere = new Sphere(mousePosition, new Vector2(100, 100), 20, randomColor);
            spheres.Add(newSphere);
        }

        // Update spheres
        foreach (Sphere sphere in spheres)
        {
            sphere.position.X += sphere.velocity.X * Time.Delta;
            sphere.position.Y += sphere.velocity.Y * Time.Delta;

            // Check for collisions with window bounds
            if (sphere.position.X - sphere.radius < 0 || sphere.position.X + sphere.radius > Raylib.GetScreenWidth())
            {
                sphere.velocity.X = -sphere.velocity.X;
            }

            if (sphere.position.Y - sphere.radius < 0 || sphere.position.Y + sphere.radius > Raylib.GetScreenHeight())
            {
                sphere.velocity.Y = -sphere.velocity.Y;
            }

            // Check for collisions with other spheres
            foreach (Sphere other in spheres)
            {
                if (sphere != other && CheckCollisionSpheres(sphere, other))
                {
                    // Handle sphere collision here
                    ResolveSphereCollision(sphere, other);
                }
            }
        }
    }

    protected internal override void Draw()
    {
        foreach (Sphere sphere in spheres)
        {
            Raylib.DrawCircle((int)sphere.position.X, (int)sphere.position.Y, sphere.radius, sphere.color);
        }
    }
    
    static bool CheckCollisionSpheres(Sphere a, Sphere b)
    {
        float dx = b.position.X - a.position.X;
        float dy = b.position.Y - a.position.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        return distance < a.radius + b.radius;
    }

    static void ResolveSphereCollision(Sphere a, Sphere b)
    {
        // Calculate the relative velocity
        Vector2 relativeVelocity = new Vector2(a.velocity.X - b.velocity.X, a.velocity.Y - b.velocity.Y);
        Vector2 normal = new Vector2(b.position.X - a.position.X, b.position.Y - a.position.Y);
        float distance = (float)Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y);

        // Normalize the collision normal
        if (distance != 0)
        {
            normal.X /= distance;
            normal.Y /= distance;
        }

        // Calculate the impulse
        float impulse = 2 * Vector2.Dot(relativeVelocity, normal) / (1 / a.radius + 1 / b.radius);

        // Apply the impulse to the spheres
        a.velocity.X += impulse / a.radius * normal.X * Time.Delta;
        a.velocity.Y += impulse / a.radius * normal.Y * Time.Delta;
        b.velocity.X -= impulse / b.radius * normal.X * Time.Delta;
        b.velocity.Y -= impulse / b.radius * normal.Y * Time.Delta;
    }
}

public class Sphere
{
    public Vector2 position;
    public Vector2 velocity;
    public float radius;
    public Color color;

    public Sphere(Vector2 position, Vector2 velocity, float radius, Color color)
    {
        this.position = position;
        this.velocity = velocity;
        this.radius = radius;
        this.color = color;
    }
}