namespace ProcessingPipeline.Tests.Fixtures
{
    using System;
    using System.Threading.Tasks;
    using Ductus.FluentDocker.Builders;
    using Ductus.FluentDocker.Common;
    using Ductus.FluentDocker.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    // it was obvious thou our experience with mongo that testing with IList<T> wont cut it
    // because IMongoQueriable<T> behaved differently than IQueriable<T> because of that
    // its beneficial to run mongo docker image every time unit test is executed and use 
    // real MongoDb to make sure it will work as intended.
    public class MongoDbFixture : IAsyncLifetime
    {
        private IContainerService containerService;
        private const int Port = 27020;
        private const int ContainerPort = 27017;

        public Uri Url => new Uri($"http://localhost:{Port}");

        public ServiceCollection ServiceCollection { get; set; }

        public async Task InitializeAsync()
        {
            try
            {
                this.containerService = new Builder()
                    .UseContainer()
                    .WithName("mongo-tests")
                    .UseImage("mongo")
                    .KeepRunning()
                    .ReuseIfExists()
                    .ExposePort(Port, ContainerPort)
                    //.WaitForPort($"{ContainerPort}/tcp", 5000, "127.0.0.1")
                    .Build();

                this.containerService.Start();
            }
            catch (FluentDockerException ex) when (ex.Message.Contains("Error response from daemon: Conflict"))
            {
                // no-op
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
