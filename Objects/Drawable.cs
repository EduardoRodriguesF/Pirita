using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public abstract class Drawable {
        private float _rotation;

        public int zIndex;
        public bool Visible { get; set; } = true;
        public virtual Vector2 InitialPosition { get; set; }
        public virtual Vector2 InitialScale { get; set; }
        public virtual Vector2 Position { get; set; }
        public float Opacity { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation {
            get => _rotation;
            set => _rotation = -value * MathHelper.PiOver4;
        }
        public Vector2 Origin { get; set; }

        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
