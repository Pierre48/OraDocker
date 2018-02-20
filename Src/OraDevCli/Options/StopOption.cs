using System;
using CommandLine;
using OraDevCli.Services;

namespace OraDevCli.Options
{
    [Verb("stop", HelpText = "stop an existing database", Hidden = false)]
    public class StopOption :OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Stopping a database...");
            base.Run();

            using (var client = new DockerService())
            {
                var container = client.GetContainer(true, $"^.*{ContainerKey}-{User}-{Name}$");

                if (container == null)
                {
                    Console.WriteErrorLine($"A {ContainerKey} database named {Name} does not exist for user {User}");
                    return;
                }
                if (container.State != "running")
                {
                    Console.WriteErrorLine($"A {ContainerKey} database named {Name} exists for user {User}, but its status is not equal to running");
                    return;
                }

                client.StopContainer(container.ID);
                Console.WriteSuccessLine("The database was successfully stopped.");
            }
        }
    }
}