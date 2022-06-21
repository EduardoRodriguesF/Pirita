using Microsoft.Xna.Framework;

namespace Pirita.ECS {
    public class VelocityComponent : Component {
        public float Speed { get; set; }
        public Vector2 Velocity { get; set; }

        public void Deconstruct(out float speed, out Vector2 velocity) {
            speed = Speed;
            velocity = Velocity;
        }
    }
}