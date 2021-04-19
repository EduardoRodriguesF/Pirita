using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pirita.Engine.Scenes;

namespace Pirita {
    public class PiritaGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        private Scene _currentScene;
        private Scene _firstScene;

        private int _DesignedResolutionWidth;
        private int _DesignedResolutionHeight;
        private float _designedResolutionAspectRatio;

        public PiritaGame(int width, int height, Scene firstScene) {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _firstScene = firstScene;
            _DesignedResolutionWidth = width;
            _DesignedResolutionHeight = height;
            _designedResolutionAspectRatio = width / (float)height;
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth = _DesignedResolutionWidth;
            _graphics.PreferredBackBufferHeight = _DesignedResolutionHeight;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, _DesignedResolutionWidth, _DesignedResolutionHeight, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle() {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= _designedResolutionAspectRatio) {
                var presentHeight = (int)(Window.ClientBounds.Width / _designedResolutionAspectRatio + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            } else {
                var presentWidth = (int)(Window.ClientBounds.Height * _designedResolutionAspectRatio + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;
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
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentScene.Render(_spriteBatch);

            _spriteBatch.End();

            // Render scaled content
            _graphics.GraphicsDevice.SetRenderTarget(null);
            _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
