using System;

namespace Pirita {
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new PiritaGame())
                game.Run();
        }
    }
}
