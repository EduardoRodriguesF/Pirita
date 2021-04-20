using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Engine.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Components {
    public class Player : Component {
        private const float Speed = 2f;

        public Vector2 Velocity;

        private bool _movingLeft, _movingRight;

        public void MoveLeft() {
            _movingLeft = true;
        }

        public void MoveRight() {
            _movingRight = true;
        }

        protected override void Animate() {
            _animationManager.Play(_animations[1]);
        }

        public override void Update(GameTime gameTime) {
            var ml = _movingLeft ? 1 : 0;
            var mr = _movingRight ? 1 : 0;

            Velocity.X = (mr - ml) * Speed;

            Position += Velocity;

            _movingLeft = false;
            _movingRight = false;

            Animate();
            _animationManager.Position = _position;
            _animationManager.Update(gameTime);
        }
    }
}
