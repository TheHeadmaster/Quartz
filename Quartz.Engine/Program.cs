using System;

namespace Quartz.Engine
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (GameCore? game = new GameCore())
            {
                game.Run();
            }
        }
    }
}