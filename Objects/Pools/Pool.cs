using System;
using System.Collections;

namespace Pirita.Pools {
    public interface IPoolable {
        // Reset object's properties to their initial state.
        void Initialize();

        // Called when an object is returned to the pool.
        void Release();

        // Checks if the object came from the pool.
        bool PoolIsValid { get; set; }

        // Ensures object isn't freed twice.
        bool PoolIsFree { get; set; }
    }

    public class Pool<T> where T : IPoolable, new() {
        private Stack _stack;
        private int _capacity; // Variable for debug purpose

        public Pool(int capacity) {
            _stack = new Stack(capacity);
            for (int i = 0; i < capacity; i++) {
                AddNewObject();
            }
        }

        // Adds a new object to the pool (usually only used when pool is constructed
        private void AddNewObject() {
            T obj = new T() {
                PoolIsValid = true,
            };
            _stack.Push(obj);
            _capacity++;
        }

        // Releases an object from the pool
        public void Release(T obj) {
            if (obj.PoolIsFree) return;

            if (obj.PoolIsFree)
                throw new Exception($"POOL {this}: Object already release [{obj}]");
            else if (!obj.PoolIsValid)
                throw new Exception($"POOL {this}: Object not valid [{obj}]");

            obj.Release();
            obj.PoolIsFree = true;
            _stack.Push(obj);
        }

        // Get an object from the pool (creates an object if the pool has become depleted).
        public T Get() {
            if (_stack.Count == 0) {
                AddNewObject();
            }

            T obj = (T)_stack.Pop();
            obj.Initialize();
            obj.PoolIsFree = false;
            return obj;
        }
    }
}
