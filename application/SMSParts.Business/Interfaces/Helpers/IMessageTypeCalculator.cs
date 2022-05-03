using static SMSParts.Domain.Common.Enums;

namespace SMSParts.Business.Interfaces.Helpers
{
    public interface IMessageTypeCalculator
    {
        SmsTextType GetSmsTextType(bool nonStandardCharacters, int totalCharactersNeeded);
    }
}
