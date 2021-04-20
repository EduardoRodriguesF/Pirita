using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Scenes;
using Pirita.SampleGame.Components;
using Pirita.SampleGame.Scenes.Gameplay.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay {
    public class GameplayScene : Scene {
        private Texture2D _playerTexture;
        private Player _player;

        public override void LoadContent() {
            _playerTexture = LoadTexture("Sprites/Player/idle");
            _player = new Player(_playerTexture);
            AddComponent(_player);
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

            HandleInput(gameTime);
            foreach (var c in _components) {
                c.Update(gameTime);
            }
        }

        protected override void SetInputManager() {
            InputManager = new Engine.Input.InputManager(new GameplayInputMapper());
        }
    }
}
