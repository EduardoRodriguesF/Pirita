using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Input;
using Pirita.Objects;
using Pirita.Pools;
using Pirita.Sound;
using Pirita.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pirita.Pools.IPoolable;

namespace Pirita.Scenes {
    public abstract class Scene {
        private bool _debug = false;

        private ContentManager _contentManager;
        private Viewport _viewport;
        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public Viewport Viewport { get => _viewport; set => _viewport = value; }
        public Color BackgroundColor { get; protected set; } = Color.CornflowerBlue;

        protected InputManager InputManager { get; set; }
        protected SoundManager SoundManager { get; set; }
        protected LayerManager LayerManager { get; set; }

        protected Camera Camera { get; set; }
        protected Hud Hud { get; set; }

        protected Rectangle RenderArea;

        public event EventHandler<Scene> OnSceneSwitched;
        public event EventHandler<Event> OnEventNotification;

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight) {
            _contentManager = contentManager;
            _viewport = new Viewport(0, 0, viewportWidth, viewportHeight);

            RenderArea = new Rectangle(0, 0, viewportWidth, viewportHeight);

            SetInputManager();
            SetSoundManager();
            SetLayerManager();
            SetCamera();
            SetHud();
        }

        public abstract void LoadContent();

        public virtual void HandleInput(GameTime gameTime) {
            if (InputManager == null) return;

            InputManager.GetCommands(command => {
                if (command is InputCommand.DebugToggle) {
                    NotifyEvent(new Event.DebugToggle());
                } else if (command is InputCommand.FullscreenToggle) {
                    NotifyEvent(new Event.FullscreenToggle());
                }
            });
        }

        protected abstract void SetInputManager();
        protected virtual void SetSoundManager() {
            SoundManager = new SoundManager();
        }

        public virtual void SetLayerManager() {
            LayerManager = new LayerManager();
        }

        protected virtual void SetCamera() {
            Camera = new Camera(_viewport);
        }

        protected virtual void SetHud() {
            Hud = new Hud(_viewport);
        }

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

            Camera.UpdateCamera(_viewport);
            Hud.Update(Camera.Position, Camera.Zoom);
        }

        protected void UpdateRenderArea() {
            RenderArea.X = (int)(Camera.Position.X - (_viewport.Width / Camera.Zoom / 2));
            RenderArea.Y = (int)(Camera.Position.Y - (_viewport.Height / Camera.Zoom / 2));
            RenderArea.Width = (int)(_viewport.Width / Camera.Zoom);
            RenderArea.Height = (int)(_viewport.Height / Camera.Zoom);
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform, blendState: BlendState.AlphaBlend);

            foreach (var layer in LayerManager.Layers) {
                foreach (var obj in layer.Objects) {
                    if (obj.Visible && RenderArea.Intersects(new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Width, obj.Height))) {
                        obj.Render(spriteBatch);
                    }
                }
            }

            Hud.Render(spriteBatch);

            if (_debug) {
                foreach (var obj in _gameObjects.Where(a => a != null).OrderBy(a => a.zIndex)) {
                    obj.RenderHitbox(spriteBatch, Color.Red, 1);
                    obj.RenderOrigin(spriteBatch, Color.Yellow, 2);
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

        protected Effect LoadEffect(string effectName) {
            return _contentManager.Load<Effect>(effectName);
        }

        protected void AddObject(GameObject gameObject) {
            AddObject(gameObject, 0);
        }

        protected void AddObject(GameObject gameObject, int depth) {
            _gameObjects.Add(gameObject);

            var layer = LayerManager.FindLayer(depth);

            layer.AddObject(gameObject);
        }

        protected void AddDrawableObject(Drawable obj, int depth) {
            var layer = LayerManager.FindLayer(depth);

            layer.AddObject(obj);
        }

        protected void RemoveObject(GameObject gameObject) {
            _gameObjects.Remove(gameObject);
            foreach (var layer in LayerManager.Layers) {
                if (layer.Objects.Contains(gameObject)) {
                    layer.RemoveObject(gameObject);
                    break;
                }
            }
        }

        protected List<T> CleanObjects<T>(List<T> objectList, Pool<T> objectPool = null) where T : GameObject, new() {
            List<T> listOfItemsToKeep = new List<T>();

            foreach (T item in objectList) {
                if (item.Destroyed) {
                    RemoveObject(item);
                    if (objectPool != null) objectPool.Release(item);
                } else listOfItemsToKeep.Add(item);
            }

            return listOfItemsToKeep;
        }

        public void ToggleDebug() {
            _debug = !_debug;
        }
    }
}
