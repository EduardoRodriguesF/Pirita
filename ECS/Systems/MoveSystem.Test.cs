using Xunit;
using Xunit.Abstractions;
using Microsoft.Xna.Framework;
using Pirita.ECS;
using System;

namespace Pirita.Tests {
    public class MoveSystemTests {
        private readonly ITestOutputHelper _output;

        public MoveSystemTests(ITestOutputHelper output) {
            _output = output;
        }

        private GameTime MockGameTime() {
            return new GameTime(
                new TimeSpan(0, 0, 1),
                new TimeSpan(0, 0, 1)
            );
        }

        [Fact]
        public void MoveSystem_UpdatesPosition() {
            var system = new MoveSystem();
            var entity = new Entity();
            var velocity = new VelocityComponent() { Velocity = new Vector2(1, 0) };
            var position = new PositionComponent();

            entity.AddComponent(velocity);
            entity.AddComponent(position);

            var gameTime = MockGameTime();
            
            _output.WriteLine($"{velocity.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds}");

            system.UpdateOnEntity(gameTime, entity);

            Assert.Equal(new Vector2(1, 0), position.Position);
        }

        [Fact]
        public void MoveSystem_UpdatesPosition_NonZeroInitialPosition() {
            var system = new MoveSystem();
            var entity = new Entity();
            var velocity = new VelocityComponent() { Velocity = new Vector2(2, 4) };
            var position = new PositionComponent() { Position = new Vector2(1, 2) };

            entity.AddComponent(velocity);
            entity.AddComponent(position);

            var gameTime = MockGameTime();

            system.UpdateOnEntity(gameTime, entity);

            Assert.Equal(new Vector2(3, 6), position.Position);
        }

        [Fact]
        public void MoveSystem_UpdatesPosition_MultipleTimes() {
            var system = new MoveSystem();
            var entity = new Entity();
            var velocity = new VelocityComponent() { Velocity = new Vector2(1, 0) };
            var position = new PositionComponent();

            entity.AddComponent(velocity);
            entity.AddComponent(position);

            var gameTime = MockGameTime();

            system.UpdateOnEntity(gameTime, entity);
            system.UpdateOnEntity(gameTime, entity);
            system.UpdateOnEntity(gameTime, entity);

            Assert.Equal(new Vector2(3, 0), position.Position);
        }
    }
}