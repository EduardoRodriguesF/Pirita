using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Objects {
    public class Text : Drawable {
        public SpriteFont Font;
        public string Str;
        public Color Color;
        public float Scale;
        public float Rotation;
        public Vector2 Origin;

        public Text(string text, SpriteFont font, Color color, float scale = 1, float rotation = 0) {
            Str = text;
            Font = font;
            Scale = scale;
            Color = color;
            Rotation = rotation;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Font, Str, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, zIndex);
        }
    }
}
