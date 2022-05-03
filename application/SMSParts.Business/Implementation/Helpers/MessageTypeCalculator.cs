using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;
using static SMSParts.Domain.Common.Enums;

namespace SMSParts.Business.Implementation.Helpers
{
    public class MessageTypeCalculator : IMessageTypeCalculator
    {
        public SmsTextType GetSmsTextType(bool nonStandardCharacters, int totalCharactersNeeded)
        {
            if (SinglePartWithNonStandardCharacters(nonStandardCharacters, totalCharactersNeeded))
            {
                return SmsTextType.NonStandardGsmSinglePartMessage;
            }

            if (MultiPartWithNonStandardCharacters(nonStandardCharacters, totalCharactersNeeded))
            {
                return SmsTextType.NonStandardGsmMultiPartMessage;
            }

            if (SinglePartWithStandardCharacters(nonStandardCharacters, totalCharactersNeeded))
            {
                return SmsTextType.StandardGsmSinglePartMessage;
            }

            if (MultiPartWithStandardCharacters(nonStandardCharacters, totalCharactersNeeded))
            {
                return SmsTextType.StandardGsmMultiPartMessage;
            }

            return SmsTextType.Unknown;
        }

        private static bool MultiPartWithStandardCharacters(bool hasNonStandardCharacters, int totalCharactersNeeded)
        {
            return !hasNonStandardCharacters && totalCharactersNeeded > GlobalVariables.SmsParts.SingleStandardGsm;
        }

        private static bool SinglePartWithStandardCharacters(bool hasNonStandardCharacters, int totalCharactersNeeded)
        {
            return !hasNonStandardCharacters && totalCharactersNeeded <= GlobalVariables.SmsParts.SingleStandardGsm;
        }

        private static bool MultiPartWithNonStandardCharacters(bool hasNonStandardCharacters, int totalCharactersNeeded)
        {
            return hasNonStandardCharacters && totalCharactersNeeded > GlobalVariables.SmsParts.SingleNonStandardGsm;
        }

        private static bool SinglePartWithNonStandardCharacters(bool hasNonStandardCharacters, int totalCharactersNeeded)
        {
            return hasNonStandardCharacters && totalCharactersNeeded <= GlobalVariables.SmsParts.SingleNonStandardGsm;
        }
    }
}
