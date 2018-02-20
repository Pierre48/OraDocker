using CommandLine;
using OraDevCli.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraDevCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CreateOption, ClearOption, RemoveOption,ListOption>(args)
     .WithParsed<IOption>(opts => opts.Run())
     .WithNotParsed(errs => Console.WriteErrorLine($"Unknown verb"));
        }
    }
}
