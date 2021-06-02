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
        protected readonly List<GameObject> _gameObjects = new List<GameObject>();

        protected InputManager InputManager { get; set; }
        protected SoundManager SoundManager { get; set; }

        protected Camera Camera { get; set; }

        protected Rectangle RenderArea;

        public event EventHandler<Scene> OnSceneSwitched;
        public event EventHandler<Event> OnEventNotification;

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight) {
            _contentManager = contentManager;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;

            RenderArea = new Rectangle(0, 0, viewportWidth, viewportHeight);

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

            foreach (var obj in _gameObjects) {
                if (obj != null) {
                    obj.OnNotify(gameEvent);
                }
            }
        }

        protected void SwitchScene(Scene scene) {
            OnSceneSwitched?.Invoke(this, scene);
        }

        protected virtual void UpdateObjects(GameTime gameTime) {
            foreach (var gameObject in _gameObjects) {
                gameObject.Update(gameTime);
            }
        }

        protected virtual void PostUpdateObjects(GameTime gameTime) {
            foreach (var obj in _gameObjects) {
                obj.PostUpdate(gameTime);
            }
        }

        public abstract void UpdateGameState(GameTime gameTime);

        public void Update(GameTime gameTime) {
            if (InputManager != null) InputManager.Update();
            HandleInput(gameTime);

            UpdateRenderArea();

            UpdateGameState(gameTime);
        }

        protected void UpdateRenderArea() {
            RenderArea.X = (int)(Camera.Position.X - (_viewportWidth / Camera.Zoom / 2));
            RenderArea.Y = (int)(Camera.Position.Y - (_viewportHeight / Camera.Zoom / 2));
            RenderArea.Width = (int)(_viewportWidth / Camera.Zoom);
            RenderArea.Height = (int)(_viewportHeight / Camera.Zoom);
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform, blendState: BlendState.AlphaBlend);
            var orderedList = _gameObjects.Where(a => a != null).OrderBy(a => a.zIndex);

            foreach (var obj in orderedList) {
                if (RenderArea.Intersects(new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Width, obj.Height))) {
                    obj.Render(spriteBatch);

                    if (_debug) {
                        obj.RenderHitbox(spriteBatch, Color.Red, 1);
                        obj.RenderOrigin(spriteBatch, Color.Yellow, 2);
                    }
                }
            }

            if (_debug) {
                foreach (var obj in orderedList) {
                    obj.RenderHitbox(spriteBatch, Color.Red, 1);
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
            _gameObjects.Add(gameObject);
        }

        protected void RemoveObject(GameObject gameObject) {
            _gameObjects.Remove(gameObject);
        }

        protected List<T> CleanObjects<T>(List<T> objectList) where T : GameObject {
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
