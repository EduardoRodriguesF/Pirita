using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pirita.ECS;

public class ComponentSystem {
    private List<Entity> _validEntities = new List<Entity>();

    public List<Type> RequiredComponents { get; private set; } = new List<Type>();

    public void CheckForValidEntities(List<Entity> entities) {
        _validEntities.Clear();

        foreach (var entity in entities) {
            if (!HasRequiredComponents(entity)) return;

            _validEntities.Add(entity);
        }
    }

    public bool HasRequiredComponents(Entity entity) {
        foreach (var type in RequiredComponents) {
            if (!entity.HasComponent(type)) {
                return false;
            }
        }

        return true;
    }

    public bool AddEntityIfValid(Entity entity) {
        if (!_validEntities.Contains(entity) || !HasRequiredComponents(entity))
            return false;

        entity.ComponentsChanged += OnComponentsChanged;
        _validEntities.Add(entity);

        return true;
    }

    public void OnComponentsChanged(object sender, EventArgs e) {
        var entity = sender as Entity;

        if (!HasRequiredComponents(entity)) {
            _validEntities.Remove(entity);
            return;
        };

        _validEntities.Add(entity);
    }

    public virtual void Update(GameTime gameTime) {
        foreach (var entity in _validEntities) {
            UpdateOnEntity(gameTime, entity);
        }
    }

    public virtual void UpdateOnEntity(GameTime gameTime, Entity entity) { }
}
