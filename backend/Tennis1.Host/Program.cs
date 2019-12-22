using System;
using Regulus.Utility.WindowConsoleAppliction;
namespace Tennis1.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Regulus.Remote.Soul.Console.Application(args);
            app.Run();
        }
    }
}
