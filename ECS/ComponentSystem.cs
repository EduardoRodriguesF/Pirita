using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pirita.ECS {
    public class ComponentSystem {
        public List<Type> RequiredComponents { get; private set; } = new List<Type>();

        public bool HasRequiredComponents(Entity entity) {
            foreach (var type in RequiredComponents) {
                if (!entity.HasComponent(type)) {
                    return false;
                }
            }

            return true;
        }

        public virtual void Update(GameTime gameTime, Entity entity) { }
    }
}