using Microsoft.Xna.Framework;

namespace Pirita.ECS;

public class PositionComponent : Component {
    public Vector2 Position { get; set; }

    public PositionComponent() {
        Position = Vector2.Zero;
    }

    public PositionComponent(Vector2 position) {
        Position = position;
    }

    public PositionComponent(float x, float y) {
        Position = new Vector2(x, y);
    }

    public void Deconstruct(out Vector2 position) {
        position = Position;
    }
}
