using System;
using System.Collections.Generic;

namespace Pirita.ECS {
    public class Entity {
        public bool Destroyed { get; private set; } = false;

        public bool Enabled = true;

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

            return component;
        }

        public Component GetComponent(Type type) {
            return _componentDictionary[type];
        }

        public bool HasComponent(Type type) {
            return _componentDictionary.ContainsKey(type);
        }

        public List<Component> GetAllComponent() {
            return _componentList;
        }

        public Component RemoveComponent(Type type) {
            if (_componentDictionary.TryGetValue(type, out Component component)) {
                component.Destroy();
                _componentDictionary.Remove(type);
                _componentList.Remove(component);
                component.Owner = null;

                return component;
            }

            return null;
        }
    }
}
