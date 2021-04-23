using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Components.Collision {
    public class Hitbox {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }

        public Hitbox(Vector2 position, float width, float height) {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(Hitbox otherHB) {
            return CollidesWith(otherHB, Position);
        }

        public bool CollidesWith(Hitbox otherHB, Vector2 pos) {
            return 
                (pos.X < otherHB.Position.X + otherHB.Width &&
                pos.X + Width > otherHB.Position.X &&
                pos.Y < otherHB.Position.Y + otherHB.Height &&
                pos.Y + Height > otherHB.Position.Y);
        }

        public bool CollidesWith(Vector2 p) {
            return
                (p.X < Position.X + Width &&
                p.X > Position.X &&
                p.Y < Position.Y + Height &&
                p.Y > Position.Y);
        }
    }
}
