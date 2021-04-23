using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Scenes;
using Pirita.Engine.Components.Animations;
using System;
using System.Collections.Generic;
using Pirita.Engine.Components.Collision;

namespace Pirita.Engine.Components {
    public class Component {
        protected List<Texture2D> _textures;
        protected List<Animation> _animations;
        protected AnimationManager _animationManager;

        protected Texture2D _hitboxTexture;

        protected Vector2 _position;

        protected List<Hitbox> _hitboxes = new List<Hitbox>();

        public int zIndex;
        public event EventHandler<Event> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public virtual int Width { get { return _textures[0].Width; } }
        public virtual int Height { get { return _textures[0].Height; } }

        public virtual Vector2 Position {
            get { return _position; }
            set {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

                if (_animationManager != null) _animationManager.Position = _position;

                foreach (var hb in _hitboxes) {
                    hb.Position = new Vector2(hb.Position.X + deltaX, hb.Position.Y + deltaY);
                }
            }
        }

        public List<Hitbox> Hitboxes {
            get {
                return _hitboxes;
            }
        }

        public void SetTextures(List<Texture2D> textures) {
            _textures = textures;
        }

        public void SetAnimations(List<Animation> animations) {
            _animations = animations;

            _animationManager = new AnimationManager(_animations[0]);
        }

        protected virtual void Animate() { }

        public virtual void OnNotify(Event gameEvent) { }
        public void SendEvent(Event gameEvent) {
            OnObjectChanged?.Invoke(this, gameEvent);
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void PostUpdate(GameTime gameTime) { }

        public virtual void Render(SpriteBatch spriteBatch) {
            if (!Destroyed) {
                if (_animationManager != null) {
                    _animationManager.Render(spriteBatch, 1);
                } else {
                    spriteBatch.Draw(_textures[0], _position, Color.White);
                }
            }
        }

        public void RenderHitbox(SpriteBatch spriteBatch, Color color, int lineWidth) {
            if (Destroyed) return;

            if (_hitboxTexture == null) {
                CreateHitboxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var hb in _hitboxes) {
                spriteBatch.Draw(_hitboxTexture, new Rectangle(hb.Rectangle.X, hb.Rectangle.Y, lineWidth, hb.Rectangle.Height + lineWidth), color);
                spriteBatch.Draw(_hitboxTexture, new Rectangle(hb.Rectangle.X, hb.Rectangle.Y, hb.Rectangle.Width + lineWidth, lineWidth), color);
                spriteBatch.Draw(_hitboxTexture, new Rectangle(hb.Rectangle.X + hb.Rectangle.Width, hb.Rectangle.Y, 1, hb.Rectangle.Height + lineWidth), color);
                spriteBatch.Draw(_hitboxTexture, new Rectangle(hb.Rectangle.X, hb.Rectangle.Y + hb.Rectangle.Height, hb.Rectangle.Width + lineWidth, lineWidth), color);
            }
        }

        public void AddHitbox(Hitbox hb) {
            _hitboxes.Add(hb);
        }

        private void CreateHitboxTexture(GraphicsDevice graphicsDevice) {
            _hitboxTexture = new Texture2D(graphicsDevice, 1, 1);
            _hitboxTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void Destroy() {
            Destroyed = true;
        }
    }
}
