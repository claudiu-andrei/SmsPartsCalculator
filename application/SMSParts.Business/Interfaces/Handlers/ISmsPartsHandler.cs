using SMSParts.Domain.Models;

namespace SMSParts.Business.Interfaces.Handlers
{
    public interface ISmsPartsHandler
    {
        SmsPartsInformationDto GetSmsParts(string text);
    }
}
