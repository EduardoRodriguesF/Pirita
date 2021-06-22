using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.EC {
    public abstract class Component {
        public int zIndex;

        public Entity Owner;

        public bool Enabled = true;
        public bool Visible = false;

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Render(SpriteBatch spriteBatch);
        public abstract void Destroy();
    }
}
