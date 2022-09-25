namespace Pirita.ECS;

public abstract class Component {
    public Entity Owner;
    public bool Enabled = true;
}
