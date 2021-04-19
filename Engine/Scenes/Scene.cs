using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Components;
using Pirita.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirita.Engine.Scenes {
    public abstract class Scene {
        private ContentManager _contentManager;
        protected int _viewportWidth, _viewportHeight;
        private readonly List<Component> _components = new List<Component>();

        protected InputManager InputManager { get; set; }

        public event EventHandler<Scene> OnSceneSwitched;
        public event EventHandler<Event> OnEventNotification;

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight) {
            _contentManager = contentManager;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;

            SetInputManager();
        }

        public abstract void LoadContent();

        protected abstract void SetInputManager();

        public void UnloadContent() {
            _contentManager.Unload();
        }

        protected void NotifyEvent(Event gameEvent) {
            OnEventNotification?.Invoke(this, gameEvent);

            foreach (var c in _components) {
                if (c != null) {
                    c.OnNotify(gameEvent);
                }
            }
        }

        protected void SwitchScene(Scene scene) {
            OnSceneSwitched?.Invoke(this, scene);
        }

        public virtual void UpdateGameState(GameTime gameTime) {
            foreach (var c in _components) {
                c.Update(gameTime);
            }
        }

        public void Update(GameTime gameTime) {
            if (InputManager != null) InputManager.Update();

            UpdateGameState(gameTime);
        }

        public void Render(SpriteBatch spriteBatch) {
            foreach (var c in _components.Where(a => a != null).OrderBy(a => a.zIndex)) {
                c.Render(spriteBatch);
            }
        }

        protected Texture2D LoadTexture(string textureName) {
            return _contentManager.Load<Texture2D>(textureName);
        }

        protected SpriteFont LoadFont(string fontName) {
            return _contentManager.Load<SpriteFont>(fontName);
        }

        protected SoundEffect LoadSound(string soundName) {
            return _contentManager.Load<SoundEffect>(soundName);
        }

        protected void AddComponent(Component component) {
            _components.Add(component);
        }

        protected void RemoveComponent(Component component) {
            _components.Remove(component);
        }
    }
}
