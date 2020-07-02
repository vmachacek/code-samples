using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessingPipeline.Tests
{
    //helper for initializing ServiceCollection in tests
    public class ServiceCollectionProvider
    {
        public IServiceCollection GetMinimalSetup()
        {
            var configration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                     { "ApplicationInsights:InstrumentationKey", "" },
                });

            var configRoot = configration.Build();

            var collection = new ServiceCollection()
                .AddLogging();

            return collection;
        }

    }
}
