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

        protected Texture2D _debugTexture;

        protected Vector2 _position;

        protected List<Hitbox> _hitboxes = new List<Hitbox>();

        public event EventHandler<Event> OnObjectChanged;

        public bool Destroyed { get; protected set; }

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
                foreach (var hb in _hitboxes) {
                    hb.Scale = Scale;
                    hb.Origin = Origin;
                }

                return _hitboxes;
            }
        }

        public void SetTextures(List<Texture2D> textures) {
            _textures = textures;
        }

        public void SetTexture(Texture2D texture) {
            _textures = new List<Texture2D>() { texture };
        }

        public void SetAnimations(List<Animation> animations) {
            _animations = animations;

            _animationManager = new AnimationManager(_animations[0]);
        }

        public void SetAnimation(Animation animation) {
            _animations = new List<Animation>() { animation };

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
                    _animationManager.Render(spriteBatch, Origin, Scale, Opacity, Rotation);
                } else {
                    var inverted = Scale.X < 0 ? 1 : 0;
                    spriteBatch.Draw(_textures[0], _position, new Rectangle(0, 0, Width, Height), Color.White * Opacity, Rotation, Origin, new Vector2(Math.Abs(Scale.X), Math.Abs(Scale.Y)), (SpriteEffects)inverted, 0f);
                }
            }
        }

        public void RenderHitbox(SpriteBatch spriteBatch, Color color, int lineWidth) {
            if (Destroyed) return;

            if (_debugTexture == null) {
                CreateDebugTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var hb in _hitboxes) {
                int xPos = (int) (hb.Rectangle.X);
                int yPos = (int) (hb.Rectangle.Y);

                spriteBatch.Draw(_debugTexture, new Rectangle(xPos, yPos, lineWidth, hb.Rectangle.Height + lineWidth), color);
                spriteBatch.Draw(_debugTexture, new Rectangle(xPos, yPos, hb.Rectangle.Width + lineWidth, lineWidth), color);
                spriteBatch.Draw(_debugTexture, new Rectangle(xPos + hb.Rectangle.Width, yPos, 1, hb.Rectangle.Height + lineWidth), color);
                spriteBatch.Draw(_debugTexture, new Rectangle(xPos, yPos + hb.Rectangle.Height, hb.Rectangle.Width + lineWidth, lineWidth), color);
            }
        }

        public void RenderOrigin(SpriteBatch spriteBatch, Color color, int size) {
            if (Destroyed) return;

            if (_debugTexture == null) {
                CreateDebugTexture(spriteBatch.GraphicsDevice);
            }

            spriteBatch.Draw(_debugTexture, new Rectangle((int)(Position.X), (int)(Position.Y), size, size), color);
        }

        public void AddHitbox(Hitbox hb) {
            _hitboxes.Add(hb);
        }

        private void CreateDebugTexture(GraphicsDevice graphicsDevice) {
            _debugTexture = new Texture2D(graphicsDevice, 1, 1);
            _debugTexture.SetData<Color>(new Color[] { Color.White });
        }

        public void Destroy() {
            Destroyed = true;
        }
    }
}
