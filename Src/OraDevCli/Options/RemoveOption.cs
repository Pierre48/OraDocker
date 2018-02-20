using System;
using CommandLine;
using OraDevCli.Options;

namespace OraDevCli.Options
{
    [Verb("Remove", HelpText = "Remove an existing database", Hidden = false)]
    public class RemoveOption : OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Removing a database...");
            base.Run();
            if (!Console.Confirm()) return;


            Console.WriteSuccessLine("The database was successfully removed.");
        }
    }
}