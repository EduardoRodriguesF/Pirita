using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Scenes {
    public class Event {
        public class Nothing : Event { }
        public class GameQuit : Event { }
        public class GameTick : Event { }
    }
}
