using Pirita.ECS;

namespace Pirita.Tests;

class SampleComponent : Component { }
class WeirdComponent : Component { }

class SampleSystem : ComponentSystem {
    public SampleSystem() {
        RequiredComponents.Add(typeof(SampleComponent));
    }
}

public class ComponentSystemTests {
    [Fact]
    public void ComponentSystem_HasRequiredComponents_ReturnsTrue_ValidEntity() {
        Entity entity = new();
        entity.AddComponent(new SampleComponent());

        SampleSystem sampleSystem = new();
        
        bool result = sampleSystem.HasRequiredComponents(entity);

        Assert.True(result);
    }

    [Fact]
    public void ComponentSystem_HasRequiredComponents_ReturnsTrue_ValidEntity_HavingExtraComponents() {
        Entity entity = new();
        entity.AddComponent(new SampleComponent());
        entity.AddComponent(new WeirdComponent());

        SampleSystem sampleSystem = new();
        
        bool result = sampleSystem.HasRequiredComponents(entity);

        Assert.True(result);
    }

    [Fact]
    public void ComponentSystem_HasRequiredComponents_ReturnsFalse_InvalidEntity() { 
        Entity entity = new();
        entity.AddComponent(new WeirdComponent());

        SampleSystem sampleSystem = new();
        
        bool result = sampleSystem.HasRequiredComponents(entity);

        Assert.False(result);
    }
}
