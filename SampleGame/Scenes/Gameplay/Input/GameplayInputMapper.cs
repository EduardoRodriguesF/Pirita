using Microsoft.Xna.Framework.Input;
using Pirita.Engine.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.SampleGame.Scenes.Gameplay.Input {
    public class GameplayInputMapper : InputMapper {
        public override IEnumerable<InputCommand> GetKeyboardState(KeyboardState state, KeyboardState oldState) {
            var commands = new List<GameplayInputCommand>();

            if (Holding(Keys.Left, state)) {
                commands.Add(new GameplayInputCommand.MoveLeft());
            }

            if (Holding(Keys.Right, state)) {
                commands.Add(new GameplayInputCommand.MoveRight());
            }

            return commands;
        }
    }
}
