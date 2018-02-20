using System;
using CommandLine;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OraDevCli.Options
{
    [Verb("list", HelpText = "List user's database", Hidden = false)]
    public class ListOption :OptionBase
    {
        public async override void Run()
        {
            Console.WriteInfoLine("Listing user's database...");
            base.Run();
            var client = new DockerClientConfiguration(new Uri("http://localhost:2375"))
                 .CreateClient();
            var parameter = new ContainersListParameters()
            {
                All=true
            };
            client.Containers
                .ListContainersAsync(parameter)
                .ContinueWith(x => 
                {
                    foreach (var container in x.Result)
                    {
                        Console.WriteInfoLine($"{container.Names.First()} ({container.State} {container.Status})");
                    }
                    Console.WriteSuccessLine("Databases was successfully listed.");
                })
                .ContinueWith(x=> 
                {
                    Console.WriteErrorLine(x.Exception);
                }, TaskContinuationOptions.OnlyOnFaulted).Wait(); ;
           
        }
    }
}