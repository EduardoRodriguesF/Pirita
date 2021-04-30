using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using Pirita.Input;
using Pirita.Sound;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pirita.Scenes {
    public abstract class Scene {
        private bool _debug = false;

        private ContentManager _contentManager;
        protected int _viewportWidth, _viewportHeight;
        protected readonly List<GameObject> _objects = new List<GameObject>();

        protected InputManager InputManager { get; set; }
        protected SoundManager SoundManager { get; set; }

        protected Camera Camera { get; set; }

        protected Rectangle RenderArea { get; set; }

        public event EventHandler<Scene> OnSceneSwitched;
        public event EventHandler<Event> OnEventNotification;

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight) {
            _contentManager = contentManager;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;

            SetInputManager();
            SetSoundManager();
            SetCamera();
        }

        public abstract void LoadContent();

        public abstract void HandleInput(GameTime gameTime);

        protected abstract void SetInputManager();
        protected abstract void SetSoundManager();
        protected abstract void SetCamera();

        public void UnloadContent() {
            _contentManager.Unload();
        }

        protected void NotifyEvent(Event gameEvent) {
            OnEventNotification?.Invoke(this, gameEvent);

            foreach (var c in _objects) {
                if (c != null) {
                    c.OnNotify(gameEvent);
                }
            }
        }

        protected void SwitchScene(Scene scene) {
            OnSceneSwitched?.Invoke(this, scene);
        }

        protected virtual void UpdateObjects(GameTime gameTime) {
            foreach (var gameObject in _objects) {
                gameObject.Update(gameTime);
            }
        }

        protected virtual void PostUpdateObjects(GameTime gameTime) {
            foreach (var gameObject in _objects) {
                gameObject.PostUpdate(gameTime);
            }
        }

        public abstract void UpdateGameState(GameTime gameTime);

        public void Update(GameTime gameTime) {
            if (InputManager != null) InputManager.Update();
            HandleInput(gameTime);

            RenderArea = new Rectangle(
                (int)(Camera.Position.X - (_viewportWidth / Camera.Zoom / 2)),
                (int)(Camera.Position.Y - (_viewportHeight / Camera.Zoom / 2)),
                (int)(_viewportWidth / Camera.Zoom), (int)(_viewportHeight / Camera.Zoom)
            );

            UpdateGameState(gameTime);
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform, blendState: BlendState.AlphaBlend);

            foreach (var c in _objects.Where(a => a != null).OrderBy(a => a.zIndex)) {
                if (RenderArea.Intersects(new Rectangle((int)c.Position.X, (int)c.Position.Y, c.Width, c.Height))) {
                    c.Render(spriteBatch);

                    if (_debug) {
                        c.RenderHitbox(spriteBatch, Color.Red, 1);
                    }
                }
            }

            foreach (var c in _objects.Where(a => a != null).OrderBy(a => a.zIndex)) {
                if (_debug) {
                    c.RenderHitbox(spriteBatch, Color.Red, 1);
                }
            }

            spriteBatch.End();
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

        protected void AddObject(GameObject gameObject) {
            _objects.Add(gameObject);
        }

        protected void RemoveObject(GameObject gameObject) {
            _objects.Remove(gameObject);
        }

        protected List<T> CleanComponents<T>(List<T> objectList) where T : GameObject {
            List<T> listOfItemsToKeep = new List<T>();

            foreach (T item in objectList) {
                if (item.Destroyed) RemoveObject(item);
                else listOfItemsToKeep.Add(item);
            }

            return listOfItemsToKeep;
        }

        protected void ToggleDebug() {
            _debug = !_debug;
        }
    }
}
