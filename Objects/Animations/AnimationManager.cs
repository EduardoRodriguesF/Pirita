using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Animations {
    public class AnimationManager {
        private Animation _animation;
        private float _timer;
        public Vector2 Position { get; set; }
        private int _flipX;

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

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Render(SpriteBatch spriteBatch, float _opacity) {
            spriteBatch.Draw(_animation.Texture, Position,
                new Rectangle(
                    _animation.CurrentFrame * _animation.FrameWidth,
                    0,
                    _animation.FrameWidth,
                    _animation.FrameHeight
                ), Color.White * _opacity, 0, new Vector2(0, 0), new Vector2(1, 1), (SpriteEffects)_flipX, 0f);
        }
    }
}
