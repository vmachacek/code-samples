using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using ProcessingPipeline.Domain;

namespace ProcessingPipeline.Tests.PipelineExecutor
{
    public class PipelineExecutorTests
    {
        [Fact]
        public async Task run_all_tasks_in_pipeline()
        {
            var spy = new PipelineBuilderSpy();

            var serviceCollection = new ServiceCollectionProvider()
                .GetMinimalSetup()
                .AddTransient<ReservationPipelineExecutor>()
                .AddSingleton<IPipelineBuilder>(spy)
                .BuildServiceProvider();

            var executor = serviceCollection.GetRequiredService<ReservationPipelineExecutor>();

            await executor.StartProcessing(Guid.NewGuid(), new Reservation());

            spy.AllFinished().ShouldBe(true);
        }
    }
}
