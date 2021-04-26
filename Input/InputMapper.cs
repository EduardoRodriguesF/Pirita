using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pirita.Input {
    public class InputMapper {
        protected bool Pressed(Keys k, KeyboardState state, KeyboardState oldState) {
            return state.IsKeyDown(k) && oldState.IsKeyUp(k);
        }

        protected bool Holding(Keys k, KeyboardState state) {
            return state.IsKeyDown(k);
        }

        protected bool Released(Keys k, KeyboardState state, KeyboardState oldState) {
            return state.IsKeyUp(k) && oldState.IsKeyDown(k);
        }

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
