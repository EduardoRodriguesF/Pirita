﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pirita.Animations {
    public class AnimationManager {
        private Animation _animation;
        private float _timer;
        private int _flipX;
        private bool _animationEnded;

        public Vector2 Position { get; set; }
        public bool IsOnAnimationEnd { get => _animationEnded; }

        public Animation CurrentAnimation { get => _animation; }

        public AnimationManager(Animation animation) {
            _animation = animation;
        }

        public void InvertX(bool flip) {
            _flipX = flip ? 1 : 0;
        }

        public void Play(Animation animation) {
            if (_animation == animation) return;

            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop() {
            _timer = 0f;
            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime) {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed) {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount) {
                    if (_animation.isLooping)
                        _animation.CurrentFrame = 0;
                    else _animation.CurrentFrame--;

                    _animationEnded = true;
                } else {
                    _animationEnded = false;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch, Vector2 origin, Vector2 scale, float opacity = 1f, float rotation = 0f) {
            InvertX(scale.X < 0);
            var offset = _flipX == 1 ? _animation.InvertedOffset : _animation.RegularOffset;

            spriteBatch.Draw(_animation.Texture, Position + offset,
                new Rectangle(
                    _animation.CurrentFrame * _animation.FrameWidth,
                    0,
                    _animation.FrameWidth,
                    _animation.FrameHeight
                ), Color.White * opacity, rotation, origin, new Vector2(Math.Abs(scale.X), Math.Abs(scale.Y)), (SpriteEffects)_flipX, 0f);
        }
    }
}
