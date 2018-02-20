using System;
using CommandLine;
using System.Linq;

namespace OraDevCli.Options
{
    [Verb("Create", HelpText = "Create a new database", Hidden = false)]
    public class CreateOption : OptionBase
    {

        public override void Run()
        {
            Console.WriteInfoLine("Creating a new database...");
            base.Run();

            Console.WriteSuccessLine("The database was successfully created.");
        }
    }
}