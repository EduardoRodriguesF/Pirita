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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay {
    public class GameplayScene : Scene {
        private Texture2D _playerIdleTexture;
        private Texture2D _playerWalkTexture;

        private Player _player;
        private List<Coin> _coinList;
        private List<Solid> _solidList;

        private Texture2D _coinTexture;

        private Texture2D _staticTexture;

        public override void LoadContent() {
            _playerIdleTexture = LoadTexture("Sprites/Player/idle");
            _playerWalkTexture = LoadTexture("Sprites/Player/walk");

            _coinTexture = LoadTexture("Sprites/coin");

            _staticTexture = LoadTexture("Sprites/static");

            _coinList = new List<Coin>();
            _solidList = new List<Solid>();

            SoundManager.RegisterSound(new GameplayEvents.CoinCollected(), LoadSound("Sounds/coin"));

            CreatePlayer(32, 0);
            CreateCoin(64, 0);
            CreateCoin(92, 0);
            CreateCoin(124, 0);

            CreateSolid(0, 0);
            CreateSolid(150, 0);

            for (var i = 0; i < 50; i++) {
                var xPos = 16 * i;

                CreateSolid(xPos, 96);
            }

            for (var i = 0; i < 50; i++) {
                var xPos = 16 * i;

                CreateSolid(xPos, 112);
            }

            for (var i = 0; i < 50; i++) {
                var xPos = 16 * i;

                CreateSolid(xPos, 128);
            }

            OptimizeSolidsHitbox();
        }

        public void OptimizeSolidsHitbox() {
            foreach (var solid in _solidList) {
                if (solid.Hitboxes.Count == 0) continue;

                var solidsInSameRow = _solidList.Where(s => (
                    s.Position.Y == solid.Position.Y && s != solid &&
                    s.Position.X == solid.Position.X + solid.Hitboxes[0].Width
                ));

                foreach (var solidInSameRow in solidsInSameRow) {
                    solid.Hitboxes[0].Width += solid.Width;
                    solidInSameRow.Hitboxes.Remove(solidInSameRow.Hitboxes[0]);
                }
            }

            foreach (var solid in _solidList) {
                if (solid.Hitboxes.Count == 0) continue;

                var solidsInSameColumn = _solidList.Where(s => (
                    s.Position.X == solid.Position.X && s != solid &&
                    s.Position.Y == solid.Position.Y + solid.Hitboxes[0].Height &&
                    s.Hitboxes[0].Width == solid.Hitboxes[0].Width
                ));

                foreach (var solidInSameColumn in solidsInSameColumn) {
                    solid.Hitboxes[0].Height += solid.Height;
                    solidInSameColumn.Hitboxes.Remove(solidInSameColumn.Hitboxes[0]);
                }
            }
        }

        public override void HandleInput(GameTime gameTime) {
            InputManager.GetCommands(cmd => {
                if (cmd is GameplayInputCommand.MoveLeft) {
                    _player.MoveLeft();
                }

                if (cmd is GameplayInputCommand.MoveRight) {
                    _player.MoveRight();
                }

                if (cmd is GameplayInputCommand.Jump) {
                    _player.Jump();
                }

                if (cmd is GameplayInputCommand.ToggleDebugMode) {
                    ToggleDebug();
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime) {
            foreach (var c in _components) {
                c.Update(gameTime);
            }

            Camera.Target = _player.Position;
            Camera.UpdateCamera(new Viewport(0, 0, _viewportWidth, _viewportHeight));

            DetectCollisions();

            foreach (var c in _components) {
                c.PostUpdate(gameTime);
            }

            _coinList = CleanComponents(_coinList);
        }

        private void DetectCollisions() {
            var coinCollisionDetector = new AABBCollisionDetector<Coin, Player>(_coinList);
            var playerCollisionDetector = new AABBCollisionDetector<Solid, Player>(_solidList);

            coinCollisionDetector.DetectCollisions(_player, (coin, _) => {
                var collectEvent = new GameplayEvents.CoinCollected();
                coin.OnNotify(collectEvent);
                SoundManager.OnNotify(collectEvent);
            });


            Vector2 pos;
            int velDir;

            pos = _player.Position + new Vector2(_player.Velocity.X, 0);
            playerCollisionDetector.DetectCollisions(_player, pos, (solid, player) => {
                velDir = Math.Sign(player.Velocity.X);
                pos.X = player.Position.X + velDir;

                while (!playerCollisionDetector.DetectCollisions(player, pos)) {
                    player.Position += new Vector2(velDir, 0);

                    pos.X = _player.Position.X + velDir;
                }

                player.Velocity.X = 0;
            });

            pos = _player.Position + new Vector2(0, _player.Velocity.Y);
            playerCollisionDetector.DetectCollisions(_player, pos, (solid, player) => {
                velDir = Math.Sign(player.Velocity.Y);
                pos.Y = _player.Position.Y + velDir;

                while (!playerCollisionDetector.DetectCollisions(player, pos)) {
                    player.Position += new Vector2(0, velDir);

                    pos.Y = _player.Position.Y + velDir;
                }

                player.Velocity.Y = 0;
            });
        }

        private void CreatePlayer(int x, int y) {
            _player = new Player();

            var animations = new List<Animation>() {
                new Animation(_playerIdleTexture, 1, 0),
                new Animation(_playerWalkTexture, 3, .1f),
            };

            _player.SetAnimations(animations);

            _player.Position = new Vector2(x, y);

            AddComponent(_player);
        }

        private void CreateCoin(int x, int y) {
            var coin = new Coin();

            var animations = new List<Animation>() {
                new Animation(_coinTexture, 4, 0.3f),
            };

            coin.SetAnimations(animations);

            coin.Position = new Vector2(x, y);

            _coinList.Add(coin);
            AddComponent(coin);
        }

        private void CreateSolid(int x, int y) {
            var solid = new Solid();
            solid.Position = new Vector2(x, y);

            solid.AddHitbox(new Hitbox(solid.Position, 16, 16));

            var textures = new List<Texture2D>();
            textures.Add(_staticTexture);
            solid.SetTextures(textures);

            _solidList.Add(solid);
            AddComponent(solid);
        }

        private List<T> CleanComponents<T>(List<T> componentList) where T : Component {
            List<T> listOfItemsToKeep = new List<T>();

            foreach (T item in componentList) {
                if (item.Destroyed) RemoveComponent(item);
                else listOfItemsToKeep.Add(item);
            }

            return listOfItemsToKeep;
        }

        protected override void SetInputManager() {
            InputManager = new Engine.Input.InputManager(new GameplayInputMapper());
        }

        protected override void SetSoundManager() {
            SoundManager = new Engine.Sound.SoundManager();
        }

        protected override void SetCamera() {
            Camera = new Camera(new Viewport(0, 0, _viewportWidth, _viewportHeight));
        }
    }
}
