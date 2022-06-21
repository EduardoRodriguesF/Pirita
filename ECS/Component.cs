using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.ECS {
    public abstract class Component {
        public int Depth;

        public Entity Owner;

        public bool Enabled = true;
        public bool Visible = false;

        public abstract void Initialize();
        public abstract void Destroy();
    }
}
