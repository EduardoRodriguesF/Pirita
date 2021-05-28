using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Pirita.Collision {
    public class Hitbox {
        private Vector2 _position;
        private float _width;
        private float _height;

        public Vector2 Position {
            get => _position;
            set => _position = value;
        }
        public float Width { get => Math.Abs(_width * Scale.X); set => _width = value; }
        public float Height { get => Math.Abs(_height * Scale.Y); set => _height = value; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Origin { get; set; } = Vector2.Zero;

        public Rectangle Rectangle {
            get {
                return new Rectangle(
                    (int)(Position.X - Origin.X - (Scale.X < 0 ? Width - Origin.X : 0)), 
                    (int)(Position.Y - Origin.Y - (Scale.Y < 0 ? Height - Origin.Y : 0)), 
                    (int)Width, (int) Height
                );
            }
        }

        public Hitbox(Vector2 position, float width, float height) {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(Hitbox otherHB) {
            var pos = new Vector2(Rectangle.X, Rectangle.Y);

            return CollidesWith(otherHB, pos);
        }

        public bool CollidesWith(Hitbox otherHB, Vector2 pos) {
            var other = otherHB.Rectangle;

            return
                (pos.X < other.X + other.Width &&
                pos.X + Width > other.X &&
                pos.Y < other.Y + other.Height &&
                pos.Y + Height > other.Y);
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
