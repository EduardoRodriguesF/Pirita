using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pirita.Input {
    public class InputMapper {
        protected bool Pressed(Keys k, KeyboardState state, KeyboardState oldState) {
            return state.IsKeyDown(k) && oldState.IsKeyUp(k);
        }
        protected bool Pressed(ButtonState state, ButtonState oldState) {
            return state == ButtonState.Pressed && oldState == ButtonState.Released;
        }

        protected bool Holding(Keys k, KeyboardState state) {
            return state.IsKeyDown(k);
        }
        protected bool Holding(ButtonState state) {
            return state == ButtonState.Pressed;
        }

        protected bool Released(Keys k, KeyboardState state, KeyboardState oldState) {
            return state.IsKeyUp(k) && oldState.IsKeyDown(k);
        }
        protected bool Released(ButtonState state, ButtonState oldState) {
            return state == ButtonState.Released && oldState == ButtonState.Pressed;
        }

        public virtual IEnumerable<InputCommand> GetKeyboardState(KeyboardState state, KeyboardState oldState) {
            var commands = new List<InputCommand>();

            if (Pressed(Keys.F3, state, oldState))
                commands.Add(new InputCommand.DebugToggle());

            return commands;
        }

        public virtual IEnumerable<InputCommand> GetMouseState(MouseState state, MouseState oldState) {
            return new List<InputCommand>();
        }

        public virtual IEnumerable<InputCommand> GetGamePadState(GamePadState state, GamePadState oldState) {
            return new List<InputCommand>();
        }
    }
}
