using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pirita.Scenes {
    public class Layer {
        private readonly List<Drawable> _objects = new List<Drawable>();

        public int Depth;

        public List<Drawable> Objects {
            get => _objects.OrderBy(a => a.zIndex).ToList();
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
}
