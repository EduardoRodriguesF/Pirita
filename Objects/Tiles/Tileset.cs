using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Objects.Tiles {
    public class Tileset : Drawable {
        protected Texture2D _texture { get; private set; } // Tileset spritesheet

        public List<Tile> Tiles { get; private set; }

        // Tile size
        public override int Width => _texture.Width / 8;
        public override int Height => _texture.Height / 7;

        public Tileset(Texture2D texture) {
            _texture = texture;
        }

        public void AddTile(float x, float y) {
            new Tile(x, y);
        }

        public void AddTiles(List<Tile> tiles) {
            Tiles.AddRange(tiles);
        }

        /// <summary>Goes though each tile checking for other tiles next to each other to change its source position</summary>
        public virtual void CheckConnections() { }

        public override void Render(SpriteBatch spriteBatch) {
            foreach (var tile in Tiles) {
                spriteBatch.Draw(_texture, tile.Position, new Rectangle((int)tile.Source.X, (int)tile.Source.Y, Width, Height), Color.White);
            }
        }
    }
}
