using Microsoft.Xna.Framework;
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

        public bool DetectCollisions(A activeComponent, Action<P, A> collisionHandler = null) {
            return DetectCollisions(activeComponent, activeComponent.Position, collisionHandler);
        }

        public bool DetectCollisions(A activeComponent, Vector2 pos, Action<P, A> collisionHandler = null) {
            foreach (var passiveComponent in _passiveComponents) {
                if (DetectCollision(passiveComponent, activeComponent, pos)) {
                    if (collisionHandler != null) collisionHandler(passiveComponent, activeComponent);
                    return true;
                }
            }

            return false;
        }

        public bool DetectCollisions(IEnumerable<A> activeComponents, Action<P, A> collisionHandler = null) {
            foreach (var passiveComponent in _passiveComponents) {
                var copiedList = new List<A>();
                foreach (var activeComponent in activeComponents) {
                    copiedList.Add(activeComponent);
                }

                foreach (var activeComponent in copiedList) {
                    if (DetectCollision(passiveComponent, activeComponent)) {
                        if (collisionHandler != null) collisionHandler(passiveComponent, activeComponent);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DetectCollision(P passiveComponent, A activeComponent) {
            return DetectCollision(passiveComponent, activeComponent, activeComponent.Position);
        }

        private bool DetectCollision(P passiveComponent, A activeComponent, Vector2 pos) {
            foreach (var passiveHB in passiveComponent.Hitboxes) {
                foreach (var activeHB in activeComponent.Hitboxes) {
                    return activeHB.CollidesWith(passiveHB, pos);
                }
            }

            return false;
        }
    }
}
