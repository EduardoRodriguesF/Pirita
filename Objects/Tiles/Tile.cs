using Microsoft.Xna.Framework;
using Pirita.Pools;

namespace Pirita.Tiles {
    public class Tile : IPoolable {
        public Vector2 Position { get; set; }
        public Vector2 Source { get; set; }
        public bool PoolIsValid { get; set; }
        public bool PoolIsFree { get; set; }

        public void Initialize() { }
        public void Release() { }
    }
}
