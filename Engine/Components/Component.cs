using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Scenes;
using System;

namespace Pirita.Engine.Components {
    public class Component {
        protected Texture2D _texture;

        protected Vector2 _position;

        public int zIndex;
        public event EventHandler<Event> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public virtual int Width { get { return _texture.Width; } }
        public virtual int Height { get { return _texture.Height; } }

        public virtual Vector2 Position {
            get { return _position; }
            set {
                _position = value;
            }
        }

        public virtual void OnNotify(Event gameEvent) { }
        public void SendEvent(Event gameEvent) {
            OnObjectChanged?.Invoke(this, gameEvent);
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Render(SpriteBatch spriteBatch) {
            if (!Destroyed) {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
        }

        public void Destroy() {
            Destroyed = true;
        }
    }
}
