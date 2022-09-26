using Pirita.ECS;

namespace Pirita.Tests;

public class EntityTests {
    [Fact]
    public void Entity_RemoveComponent_RemovesDesiredComponent() {
        Entity entity = new();

        entity.AddComponent(new WeirdComponent());
        entity.AddComponent(new SampleComponent());

        Component removedComponent = entity.RemoveComponent<WeirdComponent>();

        Assert.IsType<WeirdComponent>(removedComponent);
    }

    [Fact]
    public void Entity_RemoveComponent_DoesNothingIfNoMatch() {
        Entity entity = new();

        entity.AddComponent(new SampleComponent());

        Component removedComponent = entity.RemoveComponent<WeirdComponent>();

        Assert.Null(removedComponent);
    }
}
