using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pirita.Scenes;

namespace Pirita {
    public class PiritaGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Scene _currentScene;
        private Scene _firstScene;

        private int _DesignedResolutionWidth;
        private int _DesignedResolutionHeight;
        private float _designedResolutionAspectRatio;

        public PiritaGame(int width, int height, Scene firstScene, bool isMouseVisible = false) {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = isMouseVisible;

            _firstScene = firstScene;
            _DesignedResolutionWidth = width;
            _DesignedResolutionHeight = height;
            _designedResolutionAspectRatio = width / (float)height;
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth = _DesignedResolutionWidth;
            _graphics.PreferredBackBufferHeight = _DesignedResolutionHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SwitchScene(_firstScene);
        }

        protected override void UnloadContent() {
            _currentScene?.UnloadContent();
        }

        private void CurrentScene_OnSceneSwitched(object sender, Scene scene) {
            SwitchScene(scene);
        }

        private void CurrentScene_OnEventNotification(object sender, Event e) {
            switch (e) {
                case Event.GameQuit _:
                    Exit();
                    break;
                case Event.DebugToggle _:
                    _currentScene.ToggleDebug();
                    break;
                case Event.FullscreenToggle _:
                    _graphics.IsFullScreen = !_graphics.IsFullScreen;

                    _graphics.PreferredBackBufferWidth = _graphics.IsFullScreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width : _DesignedResolutionWidth;
                    _graphics.PreferredBackBufferHeight = _graphics.IsFullScreen ? GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height : _DesignedResolutionHeight;
                    _graphics.ApplyChanges();

                    _currentScene.Viewport = _graphics.GraphicsDevice.Viewport;

                    break;
            }
        }

        private void SwitchScene(Scene scene) {
            if (_currentScene != null) {
                _currentScene.OnSceneSwitched -= CurrentScene_OnSceneSwitched;
                _currentScene.OnEventNotification -= CurrentScene_OnEventNotification;
                _currentScene.UnloadContent();
            }

            _currentScene = scene;

            _currentScene.Initialize(Content, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
            _currentScene.LoadContent();

            _currentScene.OnSceneSwitched += CurrentScene_OnSceneSwitched;
            _currentScene.OnEventNotification += CurrentScene_OnEventNotification;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(_currentScene.BackgroundColor);

            _currentScene.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
