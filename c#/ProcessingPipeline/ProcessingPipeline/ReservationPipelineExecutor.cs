namespace ProcessingPipeline
{
    using Microsoft.Extensions.Logging;
    using ProcessingPipeline.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReservationPipelineExecutor
    {
        private readonly PipelineBuilder _pipelineBuilder;
        private readonly ILogger<ReservationPipelineExecutor> _logger;
        private ReservationProcessingContext _context;
        private List<IReservationProcessingMiddleware> _pipeline;

        private readonly Func<Task> _emptyNextFunction = () => Task.FromResult(true);

        public ReservationPipelineExecutor(
            PipelineBuilder pipelineBuilder,
            ILogger<ReservationPipelineExecutor> logger
            )
        {
            _pipelineBuilder = pipelineBuilder;
            _logger = logger;
        }

        public async Task<ProcessingResult> StartProcessing(Guid draftId, Reservation reservation)
        {
            _pipeline = _pipelineBuilder.GetPipeline(reservation, PaymentType.PayPal);

            _context = new ReservationProcessingContext()
            {
                Reservation = reservation,
                DraftId = draftId
            };

            var first = _pipeline.FirstOrDefault();

            await RunAllMiddleware(first);

            return _context.ProcessingResult ?? new ProcessingResult()
            {
                IsSuccess = true
            };
        }

        private async Task RunAllMiddleware(IReservationProcessingMiddleware pipelineStep, int index = 0)
        {
            var isLast = index == _pipeline.Count - 1;

            Func<Task> nextMiddleWareFunction = isLast
                ? _emptyNextFunction
                : async () => await RunAllMiddleware(_pipeline[index], index);
            try
            {
                _logger.LogInformation($"Starting to execute {pipelineStep.GetType().Name}");
                await pipelineStep.Execute(_context, nextMiddleWareFunction);
            }
            catch (Exception e)
            {
                var middlewareTypeName = pipelineStep.GetType().Name;
                _logger.LogError(e, "Execution of reservation failed");
                throw;
            }
        }
    }
}
