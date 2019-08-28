using System;
using Serilog;

namespace Evolutio
{
#if WINDOWS || LINUX
    
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            using (var game = new Evolutio())
                game.Run();
        }
    }
#endif
}
