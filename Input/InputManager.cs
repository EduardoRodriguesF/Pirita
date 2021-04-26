using Microsoft.Xna.Framework.Input;
using System;

namespace Pirita.Input {
    public class InputManager {
        private readonly InputMapper _inputMapper;

        private KeyboardState _ks;
        private KeyboardState _oldKs;

        private MouseState _ms;
        private MouseState _oldMs;

        private GamePadState _gs;
        private GamePadState _oldGs;

        public InputManager(InputMapper inputMapper) {
            _inputMapper = inputMapper;

            _ks = Keyboard.GetState();
            _oldKs = _ks;

            _ms = Mouse.GetState();
            _oldMs = _ms;

            _gs = GamePad.GetState(0);
            _oldGs = _gs;
        }

        public void Update() {
            _oldKs = _ks;
            _ks = Keyboard.GetState();

            _oldMs = _ms;
            _ms = Mouse.GetState();

            _oldGs = _gs;
            _gs = GamePad.GetState(0);
        }

        public void GetCommands(Action<InputCommand> actOnState) {
            foreach (var state in _inputMapper.GetKeyboardState(_ks, _oldKs)) {
                actOnState(state);
            }

            foreach (var state in _inputMapper.GetMouseState(_ms, _oldMs)) {
                actOnState(state);
            }

            foreach (var state in _inputMapper.GetGamePadState(_gs, _oldGs)) {
                actOnState(state);
            }
        }
    }
}
