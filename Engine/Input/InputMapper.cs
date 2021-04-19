using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Input {
    public class InputMapper {
        public virtual IEnumerable<InputCommand> GetKeyboardState(KeyboardState state, KeyboardState oldState) {
            return new List<InputCommand>();
        }

        public virtual IEnumerable<InputCommand> GetMouseState(MouseState state, MouseState oldState) {
            return new List<InputCommand>();
        }

        public virtual IEnumerable<InputCommand> GetGamePadState(GamePadState state, GamePadState oldState) {
            return new List<InputCommand>();
        }
    }
}
