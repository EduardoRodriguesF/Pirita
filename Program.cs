using Pirita;
using System;

namespace Pirita {
    public static class Program {
        [STAThread]
        static void Main() {
            // Change "GameplayScene" to your initial scene
            using (var game = new PiritaGame(1280, 720, new GameplayScene())) {
                game.IsFixedTimeStep = true;
                game.TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
                game.Run();
            }
        }
    }
}
