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

        public Vector2 Velocity;

        private bool _movingLeft, _movingRight;
        private sbyte _direction;

        public void MoveLeft() {
            _movingLeft = true;
        }

        public void MoveRight() {
            _movingRight = true;
        }

        protected override void Animate() {
            if (Velocity.X != 0) {
                _animationManager.Play(_animations[1]);
            } else {
                _animationManager.Play(_animations[0]);
            }
            Debug.WriteLine(Velocity.X);

        }

        public override void Update(GameTime gameTime) {
            var ml = _movingLeft ? 1 : 0;
            var mr = _movingRight ? 1 : 0;

            _direction = (sbyte) (mr - ml);
            Velocity.X = _direction * Speed;

            Position += Velocity;

            Animate();
            _animationManager.Position = _position;
            _animationManager.Update(gameTime);

            _movingLeft = false;
            _movingRight = false;
        }
    }
}
