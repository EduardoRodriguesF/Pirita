using Microsoft.Xna.Framework;

namespace Pirita.ECS;

public class PositionComponent : Component {
    public Vector2 Position { get; set; }

    public void Deconstruct(out Vector2 position) {
        position = Position;
    }
}
