using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Components.Collision {
    public class AABBCollisionDetector<P, A> 
        where P : Component 
        where A : Component  {
        private IEnumerable<P> _passiveComponents;

        public AABBCollisionDetector(IEnumerable<P> passiveComponents) {
            _passiveComponents = passiveComponents;
        }

        public void DetectCollisions(A activeComponent, Action<P, A> collisionHandler) {
            foreach (var passiveComponent in _passiveComponents) {
                if (DetectCollision(passiveComponent, activeComponent)) {
                    collisionHandler(passiveComponent, activeComponent);
                }
            }
        }

        private bool DetectCollision(P passiveComponent, A activeComponent) {
            foreach (var passiveHB in passiveComponent.Hitboxes) {
                foreach (var activeHB in activeComponent.Hitboxes) {
                    return passiveHB.CollidesWith(activeHB);
                }
            }

            return false;
        }
    }
}
