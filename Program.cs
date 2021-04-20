using Pirita.Engine;
using Pirita.SampleGame.Scenes.Gameplay;
using System;

namespace Pirita {
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new PiritaGame(1280, 720, new GameplayScene()))
                game.Run();
        }
    }
}
