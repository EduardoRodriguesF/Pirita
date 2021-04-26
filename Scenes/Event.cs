using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Scenes {
    public class Event {
        public class Nothing : Event { }
        public class GameQuit : Event { }
        public class GameTick : Event { }
    }
}
