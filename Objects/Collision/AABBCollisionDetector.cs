using Microsoft.Xna.Framework;
using Pirita.Objects;
using System;
using System.Collections.Generic;

namespace Pirita.Collision {
    public class AABBCollisionDetector<P, A>
        where P : GameObject
        where A : GameObject {
        private IEnumerable<P> _passiveObjects;

        public AABBCollisionDetector(IEnumerable<P> passiveObjects) {
            _passiveObjects = passiveObjects;
        }

        public bool DetectCollisions(A activeObject, Action<P, A> collisionHandler = null) {
            return DetectCollisions(activeObject, activeObject.Position, collisionHandler);
        }

        public bool DetectCollisions(A activeObject, Vector2 pos, Action<P, A> collisionHandler = null) {
            foreach (var passiveObject in _passiveObjects) {
                if (DetectCollision(passiveObject, activeObject, pos)) {
                    if (collisionHandler != null) collisionHandler(passiveObject, activeObject);
                    return true;
                }
            }

            return false;
        }

        public bool DetectCollisions(IEnumerable<A> activeObjects, Action<P, A> collisionHandler = null) {
            foreach (var passiveObject in _passiveObjects) {
                var copiedList = new List<A>();
                foreach (var activeObject in activeObjects) {
                    copiedList.Add(activeObject);
                }

                foreach (var activeObject in copiedList) {
                    if (DetectCollision(passiveObject, activeObject)) {
                        if (collisionHandler != null) collisionHandler(passiveObject, activeObject);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DetectCollision(P passiveObject, A activeObject) {
            return DetectCollision(passiveObject, activeObject, activeObject.Position);
        }

        private bool DetectCollision(P passiveObject, A activeObject, Vector2 pos) {
            foreach (var passiveHB in passiveObject.Hitboxes) {
                foreach (var activeHB in activeObject.Hitboxes) {
                    return activeHB.CollidesWith(passiveHB, pos);
                }
            }

            return false;
        }
    }
}
