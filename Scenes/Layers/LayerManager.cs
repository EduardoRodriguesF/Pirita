using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirita.Scenes {
    public class LayerManager {
        private List<Layer> _layerList;

        public List<Layer> Layers { get => _layerList; }

        public LayerManager(int layerAmount = 1) {
            _layerList = new List<Layer>();

            for (var i = 0; i < layerAmount; i++) {
                AddLayer(i);
            }
        }

        public void AddLayer(int depth) {
            _layerList.Add(new Layer(depth));
            _layerList = _layerList.OrderBy(l => l.Depth).ToList();
        }

        public Layer FindLayer(int depth) {
            var layer = Layers.Find(l => l.Depth == depth);

            if (layer == null) {
                AddLayer(depth);
                layer = Layers.Find(l => l.Depth == depth);
            }

            return layer;
        }

        public void RemoveLayer(int depth) {
            RemoveLayer(_layerList.Find(l => l.Depth == depth));
        }

        public void RemoveLayer(Layer layer) {
            _layerList.Remove(layer);
        }

    }
}
