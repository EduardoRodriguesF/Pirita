using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pirita.Input;

public class InputEventArgs {
    public InputCommand InputCommand;

    public InputEventArgs(InputCommand inputCommand) {
        InputCommand = inputCommand;
    }
}
public class InputManager {
    private readonly InputMapper _inputMapper;

    private KeyboardState _keyboardState;
    private KeyboardState _oldKeyboardState;

    private MouseState _mouseState;
    private MouseState _oldMouseState;

    private GamePadState _gamePadState;
    private GamePadState _oldGamePadState;

    public event EventHandler<InputEventArgs> InputChanged;

    public InputManager(InputMapper inputMapper) {
        _inputMapper = inputMapper;

        _keyboardState = Keyboard.GetState();
        _oldKeyboardState = _keyboardState;

        _mouseState = Mouse.GetState();
        _oldMouseState = _mouseState;

        _gamePadState = GamePad.GetState(0);
        _oldGamePadState = _gamePadState;
    }

    public void Update() {
        _oldKeyboardState = _keyboardState;
        _keyboardState = Keyboard.GetState();

        _oldMouseState = _mouseState;
        _mouseState = Mouse.GetState();

        _oldGamePadState = _gamePadState;
        _gamePadState = GamePad.GetState(0);
    }

    public void GetCommands(Action<InputCommand> actOnState) {
        foreach (var state in _inputMapper.GetKeyboardState(_keyboardState, _oldKeyboardState)) {
            actOnState(state);
        }

        foreach (var state in _inputMapper.GetMouseState(_mouseState, _oldMouseState)) {
            actOnState(state);
        }

        foreach (var state in _inputMapper.GetGamePadState(_gamePadState, _oldGamePadState)) {
            actOnState(state);
        }
    }

    public Point GetMousePosition() {
        return _mouseState.Position;
    }

    protected virtual void OnInputChanged(InputCommand inputCommand) {
        InputChanged?.Invoke(this, new InputEventArgs(inputCommand));
    }
}
