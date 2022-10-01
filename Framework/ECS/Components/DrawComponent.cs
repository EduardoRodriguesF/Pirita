using Microsoft.Xna.Framework;

namespace Pirita.ECS;

public class DrawComponent : Component {
    public int Depth;
    public float Opacity { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
    public Vector2 Origin { get; set; }
}
