using System;
using CommandLine;

namespace OraDevCli.Options
{
    [Verb("Clear", HelpText = "Clear an existing database", Hidden = false)]
    public class ClearOption :OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Clearing a database...");
            base.Run();
            if (!Console.Confirm()) return;

            Console.WriteSuccessLine("The database was successfully cleared.");
        }
    }
}