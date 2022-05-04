using System.Threading.Tasks;
using Refit;
using SMSParts.IntegrationTests.Models;

namespace SMSParts.IntegrationTests.ApiClients
{
    public interface ISmsPartsApiClient
    {
        [Get("/SmsParts")]
        Task<ApiResponse<SmsPartsInformationDto>> Get([Query]string input);

        [Post("/SmsParts")]
        Task<ApiResponse<SmsPartsInformationDto>> GetParts([Body] InputModelDto input);
    }
}
