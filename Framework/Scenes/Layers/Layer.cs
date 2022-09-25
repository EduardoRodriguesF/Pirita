using Pirita.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Pirita.Scenes;

public class Layer {
    private readonly List<Drawable> _objects = new List<Drawable>();

    public int Depth;

    public List<Drawable> Objects {
        get => _objects.OrderBy(a => a.Depth).ToList();
    }

    public Layer(int depth) {
        Depth = depth;
    }

    public void AddObject(Drawable obj) {
        _objects.Add(obj);
    }

    public void RemoveObject(Drawable obj) {
        _objects.Remove(obj);
    }
}
