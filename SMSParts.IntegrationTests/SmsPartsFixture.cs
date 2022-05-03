using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Refit;
using SMSParts.IntegrationTests.ApiClients;
using SMSParts.IntegrationTests.Models;

namespace SMSParts.IntegrationTests
{
    public class SmsPartsFixture
    {
        private string BaseAddress = "https://localhost:55002";

        public ISmsPartsApiClient ApiClient;

        public SmsPartsFixture()
        {
            ApiClient = RestService
                .For<ISmsPartsApiClient>(BaseAddress);
        }

        public async Task<SmsPartsInformationDto> GetPartsWithAssertion(string text)
        {
            var response = await ApiClient.Get(text);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return await Task.FromResult(response.Content);
        }

    }
}
