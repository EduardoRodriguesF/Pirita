using Microsoft.Xna.Framework.Graphics;

namespace Pirita.ECS;

public class TextureComponent : Component {
    public Texture2D Texture;

    public int Width { get => Texture.Width; }
    public int Height { get => Texture.Height; }

    public TextureComponent(Texture2D texture) {
        Texture = texture;
    }

    public void Deconstruct(out Texture2D texture, out int width, out int height) {
        texture = Texture;
        width = Width;
        height = Height;
    }
}
