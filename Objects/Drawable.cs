using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public abstract class Drawable {
        public int zIndex;
        public virtual Vector2 Position { get; set; }

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
