using System;
using CommandLine;
using System.Linq;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Collections.Generic;
using OraDevCli.Services;

namespace OraDevCli.Options
{
    [Verb("create", HelpText = "Create a new database", Hidden = false)]
    public class CreateOption : OptionBase
    {

        public override void Run()
        {
            Console.WriteInfoLine("Creating a new database...");
            base.Run();
            new IdentityService().CheckUserExists(User);
            using (var client = new DockerService())
            {
                var container = client.GetContainer(true, $"^.*{ContainerKey}-{User}-{Name}$");
                if (container != null)
                {
                    Console.WriteErrorLine($"A {ContainerKey} database named {Name} already exists for the user {User}. You cannot ask for a new one.");
                    return;
                }
                client.CreateContainer("httpd", $"{ContainerKey}-{User}-{Name}");
                Console.WriteSuccessLine("The database was successfully created.");
            }
        }
    }
}