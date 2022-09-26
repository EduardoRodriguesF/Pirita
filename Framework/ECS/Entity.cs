using System;
using System.Collections.Generic;

namespace Pirita.ECS;

public class Entity {
    private Dictionary<Type, Component> _componentDictionary;
    private List<Component> _componentList;

    public bool Destroyed { get; private set; } = false;

    public bool Enabled = true;
    public List<Component> ComponentsList { get => _componentList; }

    public event EventHandler ComponentsChanged;

    public Entity() {
        _componentDictionary = new Dictionary<Type, Component>();
        _componentList = new List<Component>();
    }

    public virtual void Destroy() {
        foreach (var component in ComponentsList) {
            component.Enabled = false;
        }
    }

    public T AddComponent<T>(T component) where T : Component {
        ComponentsList.Add(component);
        _componentDictionary.Add(typeof(T), component);

        component.Owner = this;
        OnComponentsChanged();

        return component;
    }

    public T GetComponent<T>() where T : Component {
        return (T) _componentDictionary[typeof(T)];
    }

    public bool HasComponent(Type type) {
        return _componentDictionary.ContainsKey(type);
    }

    public void UpdateComponent<T>(T Component) where T : Component {
        _componentDictionary[typeof(T)] = Component;
        OnComponentsChanged();
    }

    public T RemoveComponent<T>() where T : Component {
        var type = typeof(T);

        if (_componentDictionary.TryGetValue(type, out Component component)) {
            component.Enabled = false;
            _componentDictionary.Remove(type);
            ComponentsList.Remove(component);
            component.Owner = null;
            OnComponentsChanged();

            return (T) component;
        }

        return null;
    }

    protected virtual void OnComponentsChanged() {
        ComponentsChanged?.Invoke(this, EventArgs.Empty);
    }
}
