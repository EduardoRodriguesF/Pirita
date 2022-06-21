using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Objects {
    public class Text : Drawable {
        public SpriteFont Font;
        public string Str;
        public Color Color;

        public override int Width {
            get => (int)Font.MeasureString(Str).X;
        }

        public override int Height {
            get => (int)Font.MeasureString(Str).Y;
        }

        public Text(string text, SpriteFont font, Color color, float scale = 1, float rotation = 0) {
            Str = text;
            Font = font;
            Scale = new Vector2(scale);
            Color = color;
            Rotation = rotation;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Font, Str, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }
    }
}
