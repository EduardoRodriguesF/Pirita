using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public abstract class Drawable {
        public int zIndex;
        public virtual Vector2 Position { get; set; }
        public float Opacity { get; set; } = 1f;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get => Rotation * MathHelper.PiOver4; }
        public Vector2 Origin { get; set; }

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
