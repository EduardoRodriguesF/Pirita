using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.UI {
    public class GridBox {
        public int Width;
        public int Height;
        public Vector2 Position;

        public GridBox(float x, float y, int width, int height) {
            Width = width;
            Height = height;
            Position = new Vector2(x, y);
        }

        public void RenderBounds(SpriteBatch spriteBatch) {
            var texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            var color = Color.Yellow;
            var lineWidth = 1;

            spriteBatch.Draw(texture, new Rectangle((int) Position.X, (int) Position.Y, lineWidth, Height + lineWidth), color);
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(texture, new Rectangle((int)Position.X + Width, (int)Position.Y, lineWidth, Height + lineWidth), color);
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y + Height, Width + lineWidth, lineWidth), color);
        }
    }
}
