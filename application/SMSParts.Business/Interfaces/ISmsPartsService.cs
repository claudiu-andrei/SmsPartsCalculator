using SMSParts.Domain.Models;
using SMSParts.Domain.Models.Response;

namespace SMSParts.Business.Interfaces
{
    public interface ISmsPartsService
    {
        Response<SmsPartsInformationDto> GetSmsPartsInformation(string text);
    }
}
