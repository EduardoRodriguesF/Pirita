using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Animations;
using Pirita.Collision;
using Pirita.Scenes;
using System;
using System.Collections.Generic;

namespace Pirita.Objects {
    public class GameObject : Drawable {
        protected List<Texture2D> _textures;
        protected List<Animation> _animations;
        protected AnimationManager _animationManager;

        protected Texture2D _hitboxTexture;

        protected Vector2 _position;

        protected List<Hitbox> _hitboxes = new List<Hitbox>();

        public event EventHandler<Event> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public virtual int Width {
            get {
                if (_textures != null) return _textures[0].Width;
                else if (_animationManager != null) return _animations[0].FrameWidth;
                return 0;
            }
        }
        public virtual int Height {
            get {
                if (_textures != null) return _textures[0].Height;
                else if (_animationManager != null) return _animations[0].FrameHeight;
                return 0;
            }
        }

        public override Vector2 Position {
            get { return _position; }
            set {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

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

        protected void UpdateAnimation(GameTime gameTime) {
            _animationManager.Position = Position;
            _animationManager.Update(gameTime);
        }

        public virtual void OnNotify(Event gameEvent) { }
        public void SendEvent(Event gameEvent) {
            OnObjectChanged?.Invoke(this, gameEvent);
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void PostUpdate(GameTime gameTime) { }

        public override void Render(SpriteBatch spriteBatch) {
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
