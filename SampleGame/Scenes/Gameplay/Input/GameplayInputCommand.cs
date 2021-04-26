using Pirita.Engine.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay.Input {
    public class GameplayInputCommand : InputCommand {
        public class MoveLeft : GameplayInputCommand { }
        public class MoveRight : GameplayInputCommand { }
        public class Jump : GameplayInputCommand { }

        public class ToggleDebugMode : GameplayInputCommand { }
    }
}
