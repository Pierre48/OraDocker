using System;
using CommandLine;
using OraDevCli.Services;

namespace OraDevCli.Options
{
    [Verb("start", HelpText = "Start an existing database", Hidden = false)]
    public class StartOption :OptionBase
    {
        public override void Run()
        {
            Console.WriteInfoLine("Starting a database...");
            base.Run();

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
                    Console.WriteErrorLine($"A {ContainerKey} database named {Name} exists for user {User}, but its status is already equal to running");
                    return;
                }

                client.StartContainer(container.ID);
                Console.WriteSuccessLine("The database was successfully started.");
            }
        }
    }
}