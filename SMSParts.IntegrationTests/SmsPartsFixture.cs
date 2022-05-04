using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Refit;
using SMSParts.IntegrationTests.ApiClients;
using SMSParts.IntegrationTests.Models;
using SMSParts.IntegrationTests.TestOptionSelection;

namespace SMSParts.IntegrationTests
{
    public class SmsPartsFixture
    {
        public ISmsPartsApiClient ApiClient;

        public TestOptions TestOptions;

        public SmsPartsFixture()
        {
            TestOptions = TestOptionsBuilder.Options;
            ApiClient = RestService
                .For<ISmsPartsApiClient>(TestOptions.ServiceUrl);
        }

        public async Task<SmsPartsInformationDto> GetPartsWithAssertion(string text)
        {
            var input = new InputModelDto
            {
                Text = text
            };
            var response = await ApiClient.GetParts(input);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return await Task.FromResult(response.Content);
        }

    }
}
