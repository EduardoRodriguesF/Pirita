using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirita.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.UI {
    public class Hud {
        private readonly List<Drawable> _hudElements = new List<Drawable>();

        private Viewport _viewport;

        public Hud(Viewport viewport) {
            _viewport = viewport;
        }

        public void Update(Vector2 cameraPos, float scale) {
            var scaleVect = new Vector2(scale);

            cameraPos.X -= _viewport.Width / (scale * 2);
            cameraPos.Y -= _viewport.Height / (scale * 2);

            foreach (var element in _hudElements) {
                element.Position = element.InitialPosition + cameraPos;
                element.Scale = scaleVect;
            }
        }

        public void AddElement(Drawable element) {
            element.InitialPosition = element.Position;
            _hudElements.Add(element);
        }

        public void RemoveElement(Drawable element) {
            _hudElements.Remove(element);
        }

        public void Render(SpriteBatch spriteBatch) {
            foreach (var element in _hudElements) {
                element.Render(spriteBatch);
            }
        }
    }
}
