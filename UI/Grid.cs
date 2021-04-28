using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.UI {
    public class Grid {
        private List<List<GridBox>> _boxes;

        public Grid(int horizontalBoxes, int verticalBoxes, Viewport bounds) {
            var width = bounds.Width / horizontalBoxes;
            var height = bounds.Height / verticalBoxes;

            _boxes = new List<List<GridBox>>();

            for (var i = 0; i < horizontalBoxes; i++) {
                List<GridBox> subList = new List<GridBox>();

                for (var j = 0; j < verticalBoxes; j++) {
                    var xPos = bounds.X + (width * i);
                    var yPos = bounds.Y + (height * j);

                    subList.Add(new GridBox(xPos, yPos, width, height));
                }

                _boxes.Add(subList);
            }
        }

        public GridBox GetBox(int row, int column) {
            return _boxes[row][column];
        }

        public void RenderBounds(SpriteBatch spriteBatch) {
            foreach (List<GridBox> row in _boxes) {
                foreach (GridBox gridBox in row) {
                    gridBox.RenderBounds(spriteBatch);
                }
            }
        }
    }
}
