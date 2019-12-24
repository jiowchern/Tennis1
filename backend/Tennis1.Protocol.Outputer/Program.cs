using System;

namespace Tennis1.Protocol.Outputer
{
    class Program
    {
        static void Main(string[] args)
        {
            Regulus.Utility.Log.Instance.RecordEvent += Console.WriteLine;
            if (args.Length < 3)
            {
                Console.WriteLine("Insufficient number of parameters.");
                Console.WriteLine("ex. ");
                return;
            }
            var protocolName = args[0];
            var commonPath = args[1];
            var outputDir = args[2];
            var outputProtocolPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(outputDir, "Protocols"));
            if (!System.IO.Directory.Exists(outputProtocolPath))
                System.IO.Directory.CreateDirectory(outputProtocolPath);
            var outputAdsorberPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(outputDir, "Absorbers"));
            if (!System.IO.Directory.Exists(outputAdsorberPath))
                System.IO.Directory.CreateDirectory(outputAdsorberPath);
            var inputPath = System.IO.Path.GetFullPath(commonPath);
            System.Console.WriteLine($"input : {inputPath }");
            System.Console.WriteLine($"output : {outputProtocolPath }");
            System.Console.WriteLine($"output : {outputAdsorberPath }");
            new Regulus.Remote.Protocol.CodeOutputer(System.Reflection.Assembly.LoadFile(inputPath), protocolName + "Protocol").Output(outputProtocolPath);
            new Regulus.Remote.Unity.CodeOutputer(System.Reflection.Assembly.LoadFile(inputPath), protocolName + "Agent").Output(outputAdsorberPath);
        }
    }
}
