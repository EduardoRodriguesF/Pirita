using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Animations {
    public class Animation {
        public Texture2D Texture { get; private set; }
        public Vector2 RegularOffset { get; set; }
        public Vector2 InvertedOffset { get; set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool isLooping { get; set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed, bool loop = true) {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
            isLooping = loop;
        }
    }
}
