using System;
using System.Collections;

namespace Pirita.Pools;

public interface IPoolable {
    /// <summary>Reset object's properties to their initial state.</summary>
    void Initialize();

    /// <summary>Called when an object is returned to the pool.</summary>
    void Release();

    /// <summary>Checks if the object came from the pool.</summary>
    bool PoolIsValid { get; set; }

    /// <summary>Ensures object isn't freed twice.</summary>
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

    /// <summary>Adds a new object to the pool stack</summary>
    private void AddNewObject() {
        T obj = new T() {
            PoolIsValid = true,
        };
        _stack.Push(obj);
        _capacity++;
    }

    /// <summary>Releases an object from the pool</summary>
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

    /// <summary>
    /// Get an object from the pool.
    /// Creates an object if the pool has become depleted
    /// </summary>
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
