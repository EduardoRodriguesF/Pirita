using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Components.Animations;
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

        public override void LoadContent() {
            _playerIdleTexture = LoadTexture("Sprites/Player/idle");
            _playerWalkTexture = LoadTexture("Sprites/Player/walk");

            CreatePlayer();
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
        }

        protected override void SetInputManager() {
            InputManager = new Engine.Input.InputManager(new GameplayInputMapper());
        }

        private void CreatePlayer() {
            _player = new Player();
            var animations = new List<Animation>() {
                new Animation(_playerIdleTexture, 1, 0),
                new Animation(_playerWalkTexture, 3, .1f),
            };

            _player.SetAnimation(animations);

            AddComponent(_player);
        }
    }
}
