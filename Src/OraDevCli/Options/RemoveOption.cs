﻿using System;
using CommandLine;
using OraDevCli.Options;
using OraDevCli.Services;

namespace OraDevCli.Options
{
    [Verb("remove", HelpText = "Remove an existing database", Hidden = false)]
    public class RemoveOption : OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Removing a database...");
            base.Run();
            if (!Console.Confirm()) return;

            using (var client = new DockerService())
            {
                var container = client.GetContainer(true, $"^.*{ContainerKey}-{User}-{Name}$");

                if (container == null)
                {
                    Console.WriteErrorLine($"A {ContainerKey} database named {Name} does not exist for user {User}");
                    return;
                }
                if (container.State == "running")
                {
                    client.StopContainer(container.ID);
                }
                client.RemoveContainer(container.ID);
            }

            Console.WriteSuccessLine("The database was successfully removed.");
        }
    }
}