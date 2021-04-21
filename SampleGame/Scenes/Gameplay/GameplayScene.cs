using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Components;
using Pirita.Engine.Components.Animations;
using Pirita.Engine.Components.Collision;
using Pirita.Engine.Scenes;
using Pirita.SampleGame.Components;
using Pirita.SampleGame.Scenes.Gameplay.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay {
    public class GameplayScene : Scene {
        private Texture2D _playerIdleTexture;
        private Texture2D _playerWalkTexture;

        private Player _player;
        private List<Coin> _coinList;

        private Texture2D _coinTexture;

        public override void LoadContent() {
            _playerIdleTexture = LoadTexture("Sprites/Player/idle");
            _playerWalkTexture = LoadTexture("Sprites/Player/walk");

            _coinTexture = LoadTexture("Sprites/Coin");

            _coinList = new List<Coin>();

            CreatePlayer(32, 0);
            CreateCoin(64, 0);
            CreateCoin(92, 0);
            CreateCoin(124, 0);
        }

        public override void HandleInput(GameTime gameTime) {
            InputManager.GetCommands(cmd => {
                if (cmd is GameplayInputCommand.MoveLeft) {
                    _player.MoveLeft();
                }

                if (cmd is GameplayInputCommand.MoveRight) {
                    _player.MoveRight();
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime) {
            base.UpdateGameState(gameTime);

            Camera.Target = _player.Position;
            Camera.UpdateCamera(new Viewport(0, 0, _viewportWidth, _viewportHeight));

            DetectCollisions();
        }

        private void DetectCollisions() {
            var coinCollisionDetector = new AABBCollisionDetector<Coin, Player>(_coinList);

            coinCollisionDetector.DetectCollisions(_player, (coin, _) => {
                var collectEvent = new GameplayEvents.CoinCollected();
                coin.OnNotify(collectEvent);
            });
        }

        protected override void SetInputManager() {
            InputManager = new Engine.Input.InputManager(new GameplayInputMapper());
        }

        private void CreatePlayer(int x, int y) {
            _player = new Player();

            var animations = new List<Animation>() {
                new Animation(_playerIdleTexture, 1, 0),
                new Animation(_playerWalkTexture, 3, .1f),
            };

            _player.SetAnimation(animations);

            _player.Position = new Vector2(x, y);

            AddComponent(_player);
        }

        private void CreateCoin(int x, int y) {
            var coin = new Coin();

            var animations = new List<Animation>() {
                new Animation(_coinTexture, 4, 0.3f),
            };

            coin.SetAnimation(animations);

            coin.Position = new Vector2(x, y);

            _coinList.Add(coin);
            AddComponent(coin);
        }

        protected override void SetCamera() {
            Camera = new Camera(new Viewport(0, 0, _viewportWidth, _viewportHeight));
        }
    }
}
