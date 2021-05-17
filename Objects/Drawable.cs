using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public abstract class Drawable {
        private float _opacity = 1f;
        private Vector2 _scale = Vector2.One;
        private float _rotation;
        private Vector2 _origin;

        public int zIndex;
        public virtual Vector2 Position { get; set; }
        public float Opacity { get => _opacity; set => _opacity = value; }
        public Vector2 Scale { get => _scale; set => _scale = value; }
        public float Rotation { 
            get => _rotation;
            set => _rotation = -value * MathHelper.PiOver4;
        }
        public Vector2 Origin { get => _origin; set => _origin = value; }

        public abstract void Render(SpriteBatch spriteBatch);
    }
}
