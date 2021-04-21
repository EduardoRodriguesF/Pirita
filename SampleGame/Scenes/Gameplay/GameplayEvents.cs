using Pirita.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay {
    public class GameplayEvents : Event {
        public class ComponentHitBy : GameplayEvents { }
        public class CoinCollected : GameplayEvents { }
    }
}
