using SMSParts.Domain.Common;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Interfaces.Handlers
{
    public interface ITextToSmsPartsHandlerFactory
    {
        SmsPartsInformationDto GetSmsParts(string text, Enums.SmsTextType messageTextType);
    }
}
