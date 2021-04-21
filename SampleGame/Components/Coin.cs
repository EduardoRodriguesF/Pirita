using Microsoft.Xna.Framework;
using Pirita.Engine.Components;
using Pirita.Engine.Scenes;
using Pirita.SampleGame.Scenes.Gameplay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Components {
    public class Coin : Component {
        private const int HBPosX = 0;
        private const int HBPosY = 0;
        private const int HBWidth = 16;
        private const int HBHeight = 16;

        public Coin() {
            AddHitbox(new Engine.Components.Collision.Hitbox(new Vector2(HBPosX, HBPosY), HBWidth, HBHeight));
        }

        public override void OnNotify(Event gameEvent) {
            switch (gameEvent) {
                case GameplayEvents.CoinCollected _:
                    Destroy();
                    SendEvent(new GameplayEvents.CoinCollected());
                    break;
            }
        }

        public override void Update(GameTime gameTime) {
            _animationManager.Update(gameTime);
        }
    }
}
