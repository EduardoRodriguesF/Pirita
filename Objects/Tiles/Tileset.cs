using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using System.Collections.Generic;

namespace Pirita.Tiles {
    public class Tileset : Drawable {
        protected Texture2D _texture { get; private set; } // Tileset spritesheet

        public List<Tile> Tiles { get; private set; }

        // Tile size
        public override int Width => _texture.Width / 8;
        public override int Height => _texture.Height / 6;

        public Tileset(Texture2D texture) {
            _texture = texture;
            Tiles = new List<Tile>();
        }

        public void AddTile(int x, int y) {
            Tiles.Add(new Tile(x * Width, y * Height));
            CheckConnections();
        }

        public void AddTiles(List<Tile> tiles) {
            Tiles.AddRange(tiles);
            CheckConnections();
        }

        /// <summary>Goes though each tile checking for other tiles next to each other to change its source position</summary>
        protected virtual void CheckConnections() { }

        public override void Render(SpriteBatch spriteBatch) {
            foreach (var tile in Tiles) {
                spriteBatch.Draw(_texture, tile.Position, new Rectangle((int)tile.Source.X, (int)tile.Source.Y, Width, Height), Color.White);
            }
        }
    }
}
