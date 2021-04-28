using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.UI {
    public class Grid {
        private GridBox[][] _boxes;

        public Grid(int horizontalBoxes, int verticalBoxes, Viewport bounds) {
            var width = bounds.Width / horizontalBoxes;
            var height = bounds.Height / verticalBoxes;

            for (var i = 0; i < horizontalBoxes; i++) {
                for (var j = 0; j < verticalBoxes; j++) {
                    var xPos = bounds.X + (width * i);
                    var yPos = bounds.Y + (height * j);

                    _boxes[i][j] = new GridBox(xPos, yPos, width, height);
                }
            }
        }

        public GridBox GetBox(int row, int column) {
            return _boxes[row][column];
        }

        public void RenderBounds(SpriteBatch spriteBatch) {
            foreach (GridBox[] row in _boxes) {
                foreach (GridBox gridBox in row) {
                    gridBox.RenderBounds(spriteBatch);
                }
            }
        }
    }
}
