using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProcessingPipeline.Domain;
using Scrutor;
using System;
using System.Threading.Tasks;

namespace ProcessingPipeline
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = new ServiceCollection()
                .AddLogging(options => options.AddConsole())
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelf()
                    .WithTransientLifetime())
                .BuildServiceProvider();

            var executor = provider.GetRequiredService<ReservationPipelineExecutor>();

            // this normally come from HTTP call, fetched from DB or message queue
            var data = new Reservation() { };

            var draftId = Guid.NewGuid();

            await executor.StartProcessing(draftId, data);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
