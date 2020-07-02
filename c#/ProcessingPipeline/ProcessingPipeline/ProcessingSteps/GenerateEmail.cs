using ProcessingPipeline.Services;
using System;
using System.Threading.Tasks;

namespace ProcessingPipeline.ProcessingSteps
{
    public class GenerateEmail : IReservationProcessingMiddleware
    {
        private readonly HtmlProvider _htmlProvider;
        private readonly PdfGenerator _pdfGenerator;

        public GenerateEmail(HtmlProvider htmlProvider, PdfGenerator pdfGenerator)
        {
            _htmlProvider = htmlProvider;
            _pdfGenerator = pdfGenerator;
        }

        public Func<ReservationProcessingContext, Func<Task>, Task> Execute => async (context, next) =>
        {
            //complexity of creating different email contents based on posted payload goes here
            await next();
        };
    }
}
