using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.ECS;

public class DrawComponent : Component {
    public int Depth;
    public float Opacity { get; set; } = 1f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
    public Vector2 Origin { get; set; }
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public Color Color { get; set; } = Color.White;
}
