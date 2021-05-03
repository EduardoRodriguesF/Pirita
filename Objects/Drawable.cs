using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public abstract class Drawable {
        public int zIndex;
        public virtual Vector2 Position { get; set; }
        public float Opacity { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
