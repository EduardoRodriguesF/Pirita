using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Scenes;
using Pirita.Engine.Components.Animations;
using System;
using System.Collections.Generic;

namespace Pirita.Engine.Components {
    public class Component {
        protected List<Texture2D> _textures;
        protected List<Animation> _animations;
        protected AnimationManager _animationManager;

        protected Vector2 _position;

        public int zIndex;
        public event EventHandler<Event> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public virtual int Width { get { return _textures[0].Width; } }
        public virtual int Height { get { return _textures[0].Height; } }

        public virtual Vector2 Position {
            get { return _position; }
            set {
                _position = value;
            }
        }

        public void SetTextures(List<Texture2D> textures) {
            _textures = textures;
        }

        public void SetAnimation(List<Animation> animations) {
            _animations = animations;

            _animationManager = new AnimationManager(_animations[0]);
        }

        protected virtual void Animate() { }

        public virtual void OnNotify(Event gameEvent) { }
        public void SendEvent(Event gameEvent) {
            OnObjectChanged?.Invoke(this, gameEvent);
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Render(SpriteBatch spriteBatch) {
            if (!Destroyed) {
                if (_animationManager != null) {
                    _animationManager.Render(spriteBatch, 1);
                } else {
                    spriteBatch.Draw(_textures[0], _position, Color.White);
                }
            }
        }

        public void Destroy() {
            Destroyed = true;
        }
    }
}
