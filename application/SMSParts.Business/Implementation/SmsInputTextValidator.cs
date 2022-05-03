using SMSParts.Business.Interfaces;
using SMSParts.Domain.Common;

namespace SMSParts.Business.Implementation
{
    public class ValidateSmsInputText : IValidateSmsInputText
    {
        public string Validate(bool hasNonStandardCharacters, int totalCharactersNeeded)
        {
            var partSize = hasNonStandardCharacters
                ? GlobalVariables.SmsParts.MultipleNonStandardGsm
                : GlobalVariables.SmsParts.MultipleStandardGsm;

            if (totalCharactersNeeded > GlobalVariables.SmsParts.MaxParts * partSize)
            {
                return $"Text size extends beyond allowed " +
                       $"max {GlobalVariables.SmsParts.MaxParts} sms parts.";
            }

            return string.Empty;
        }

    }
}
