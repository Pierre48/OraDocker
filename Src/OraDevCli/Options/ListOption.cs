using System;
using CommandLine;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OraDevCli.Services;

namespace OraDevCli.Options
{
    [Verb("list", HelpText = "List user's database", Hidden = false)]
    public class ListOption :OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Listing user's database...");
            base.Run();

            using (var client = new DockerService())
            {
                var containers = client.GetContainers(true, $"^.*-{User}-.*$");

                foreach (var container in containers)
                {
                    Console.WriteInfoLine($"{container.Names.First()} ({container.State} {container.Status})");
                }
                Console.WriteSuccessLine("Databases was successfully listed.");
            }
        }
    }
}