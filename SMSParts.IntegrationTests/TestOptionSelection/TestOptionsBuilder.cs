using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace SMSParts.IntegrationTests.TestOptionSelection
{
    public class TestOptionsBuilder
    {
        private static TestOptions _options;
        public static TestOptions Options => _options ?? Build();

        private static TestOptions Build()
        {
            var options = new TestOptions();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            configuration.GetSection("TestOptions").Bind(options);

            _options = options;
            return options;
        }

    }
}
