using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects.Animations {
    public class Animation {
        public Texture2D Texture { get; private set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool isLooping { get; set; }

        public Animation(Texture2D _texture, int _frameCount, float _frameSpeed) {
            Texture = _texture;
            FrameCount = _frameCount;
            FrameSpeed = _frameSpeed;

            isLooping = true;
        }
    }
}
