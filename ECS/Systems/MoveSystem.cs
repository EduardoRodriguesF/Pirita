using Microsoft.Xna.Framework;

namespace Pirita.ECS {
    public class MoveSystem : ComponentSystem {
        public MoveSystem() {
            RequiredComponents.Add(typeof(VelocityComponent));
            RequiredComponents.Add(typeof(PositionComponent));
        }

        public override void UpdateOnEntity(GameTime gameTime, Entity entity) {
            var (speed, velocity) = entity.GetComponent<VelocityComponent>();
            var position = entity.GetComponent<PositionComponent>().Position;

            position += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}