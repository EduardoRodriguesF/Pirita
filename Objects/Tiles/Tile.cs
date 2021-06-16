using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Objects.Tiles {
    public class Tile {
        public Vector2 Position { get; private set; }
        public Vector2 Source { get; set; }

        public Tile(float x, float y) {
            Position = new Vector2(x, y); 
        }
    }
}
