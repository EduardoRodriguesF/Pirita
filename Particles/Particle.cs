using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using Pirita.Pools;
using System;

namespace Pirita.Particles {
    public class Particle : Drawable, IPoolable {
        public Texture2D Texture { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 VelocityIncrease { get; set; }

        public Vector2 Gravity { get; set; }

        public float RotationVelocity { get; set; }
        public float RotationVelocityIncrease { get; set; }

        public Color Color { get; set; }

        public float Size { get; set; }
        public float SizeIncrease { get; set; }

        public int Lifespan { get; set; }

        public override int Width => Texture != null ? Texture.Width : 0;
        public override int Height => Texture != null ? Texture.Height : 0;

        public bool PoolIsValid { get; set; }
        public bool PoolIsFree { get; set; }

        public Particle() {
            Origin = new Vector2(Width / 2, Height / 2);
        }

        public void Setup(Texture2D texture, Vector2 position, float speed, float direction, Vector2 gravity, float rotation, float rotationVelocity, Color color, float size, int lifespan, float speedIncrease, float rotationIncrease, float sizeIncrease) {
            direction *= ConvertAngle.DegreesToRadianConversion;

            Texture = texture;
            Position = position;
            Velocity = new Vector2(speed * (float)(Math.Cos(direction)), speed * (float)Math.Sin(direction));
            VelocityIncrease = new Vector2(speedIncrease * (float)(Math.Cos(direction)), speedIncrease * (float)Math.Sin(direction));
            Gravity = gravity;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            RotationVelocityIncrease = rotationIncrease;
            Color = color;
            Size = size;
            SizeIncrease = sizeIncrease;
            Lifespan = lifespan;
        }

        public void Update() {
            Lifespan--;
            Position += Velocity;
            Rotation += RotationVelocity + RotationVelocityIncrease;
            Velocity += Gravity + VelocityIncrease;
            Size += SizeIncrease;
        }

        public override void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Width, Height), Color, Rotation, Origin, Size, SpriteEffects.None, 0f);
        }

        public void Initialize() { }
        public void Release() { }
    }
}
