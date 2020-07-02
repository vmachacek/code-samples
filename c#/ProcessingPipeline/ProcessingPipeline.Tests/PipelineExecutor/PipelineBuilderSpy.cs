using ProcessingPipeline;
using ProcessingPipeline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingPipeline.Tests.PipelineExecutor
{
    //Spy for PipelineBuilder, counting if every step was hit
    public class PipelineBuilderSpy : IPipelineBuilder
    {
        List<TestStep> list = new List<TestStep>();

        public bool AllFinished() => list.All(f => f.WasRun);

        public PipelineBuilderSpy()
        {
            list = new List<TestStep>()
            {
                new TestStep(),
                new TestStep(),
                new TestStep(),
                new TestStep(),
            };
        }

        public List<IReservationProcessingMiddleware> GetPipeline(Reservation reservation, PaymentType paymentType)
        {
            return list.Cast<IReservationProcessingMiddleware>().ToList();
        }

        public class TestStep : IReservationProcessingMiddleware
        {
            public bool WasRun;

            public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
            {
                WasRun = true;
                await next();
            };
        }

    }
}
