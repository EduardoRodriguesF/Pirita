using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.UI {
    public class GridBox {
        private Texture2D _boundsTexture;

        public int Width;
        public int Height;
        public Vector2 Position;

        public GridBox(float x, float y, int width, int height) {
            Width = width;
            Height = height;
            Position = new Vector2(x, y);
        }

        public void RenderBounds(SpriteBatch spriteBatch) {
            if (_boundsTexture == null) {
                _boundsTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _boundsTexture.SetData<Color>(new Color[] { Color.Yellow });
            }

            var color = Color.White;
            var lineWidth = 1;

            spriteBatch.Draw(_boundsTexture, new Rectangle((int)Position.X, (int)Position.Y, lineWidth, Height + lineWidth), color);
            spriteBatch.Draw(_boundsTexture, new Rectangle((int)Position.X, (int)Position.Y, Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_boundsTexture, new Rectangle((int)Position.X + Width, (int)Position.Y, 1, Height + lineWidth), color);
            spriteBatch.Draw(_boundsTexture, new Rectangle((int)Position.X, (int)Position.Y + Height, Width + lineWidth, lineWidth), color);
        }
    }
}
