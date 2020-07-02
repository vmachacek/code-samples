using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessingPipeline.Tests.Http
{
    //handler which is used in test scenarios, it returns canned data if it can find one. 
    public class TestMessageHandler : HttpMessageHandler
    {
        public Func<HttpRequestMessage, Task<HttpResponseMessage>> Sender { get; set; }

        public List<HttpRequestMessage> SentRequestMessages { get; } = new List<HttpRequestMessage>();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.SentRequestMessages.Add(request);

            if (this.Sender == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }

            return await this.Sender?.Invoke(request);
        }
    }
}


