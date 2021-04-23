using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Pirita.SampleGame.Components {
    public class Player : Component {
        private const float Speed = 2f;
        private const float JumpSpeed = 6f;

        private const int HBPosX = 0;
        private const int HBPosY = 0;
        private const int HBWidth = 16;
        private const int HBHeight = 16;

        public Vector2 Velocity;

        private bool _movingLeft, _movingRight;
        private sbyte _direction;

        public Player() {
            AddHitbox(new Engine.Components.Collision.Hitbox(new Vector2(HBPosX, HBPosY), HBWidth, HBHeight));
        }

        public void MoveLeft() {
            _movingLeft = true;
        }

        public void MoveRight() {
            _movingRight = true;
        }

        public void Jump() {
            Velocity.Y -= JumpSpeed;
        }

        protected override void Animate() {
            if (Velocity.X != 0) {
                _animationManager.Play(_animations[1]);
            } else {
                _animationManager.Play(_animations[0]);
            }
        }

        public override void Update(GameTime gameTime) {
            var ml = _movingLeft ? 1 : 0;
            var mr = _movingRight ? 1 : 0;

            Velocity.Y += 0.2f;

            _direction = (sbyte) (mr - ml);
            Velocity.X = _direction * Speed;

            Animate();
            _animationManager.Update(gameTime);

            _movingLeft = false;
            _movingRight = false;
        }

        public void EndUpdate() {
            Position += Velocity;
        }
    }
}
