using System;
using System.Collections.Generic;

namespace Pirita.ECS {
    public class Entity {
        public bool Destroyed { get; private set; } = false;

        public bool Enabled = true;

        public event EventHandler ComponentsChanged;

        private Dictionary<Type, Component> _componentDictionary;
        private List<Component> _componentList;

        public Entity() {
            _componentDictionary = new Dictionary<Type, Component>();
            _componentList = new List<Component>();
        }

        public virtual void Destroy() {
            foreach (var component in _componentList) {
                component.Enabled = false;
            }
        }

        public T AddComponent<T>(T component) where T : Component {
            _componentList.Add(component);
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

        public List<Component> GetAllComponent() {
            return _componentList;
        }

        public T RemoveComponent<T>() where T : Component {
            var type = typeof(T);

            if (_componentDictionary.TryGetValue(type, out Component component)) {
                component.Enabled = false;
                _componentDictionary.Remove(type);
                _componentList.Remove(component);
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
}
