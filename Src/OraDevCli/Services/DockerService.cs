using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraDevCli.Services
{
    public class DockerService : IDisposable
    {
        private DockerClient _client;

        public DockerService(string url = "")
        {
            _client = new DockerClientConfiguration(new Uri("http://localhost:2375"))
                 .CreateClient();
        }

        public IList<ContainerListResponse> GetContainers(bool all = true, string filtersName=null)
        {
            var filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    {"name",new Dictionary<string, bool> { { filtersName, true} } }
                };
            var parameter = new ContainersListParameters()
            {
                All = all,
                Filters = filters
            };
            var task = _client.Containers
                .ListContainersAsync(parameter)
                .ContinueWith(x =>
                {
                    return x.Result;
                });

            task.Wait();

            return task.Result;
        }

        internal void RemoveContainer(string id)
        {
            var parameter = new ContainerRemoveParameters();
            _client.Containers.RemoveContainerAsync(id, parameter).Wait();
        }

        public void StartContainer(string id)
        {
            var parameter = new ContainerStartParameters();
            _client.Containers.StartContainerAsync(id,parameter).Wait();
        }
        public void StopContainer(string id)
        {
            var parameter = new ContainerStopParameters();
            _client.Containers.StopContainerAsync(id, parameter).Wait();
        }

        public ContainerListResponse GetContainer(bool all = true, string filterName = null)
        {
            return GetContainers(all,filterName).FirstOrDefault();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void CreateContainer(string imageName, string containerName)
        {
                    var createParam = new CreateContainerParameters
                    {
                        Name = containerName,
                        Image = imageName
                    };
                    _client.Containers.CreateContainerAsync(createParam).Wait();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null) _client.Dispose();
            }
        }
    }
}
