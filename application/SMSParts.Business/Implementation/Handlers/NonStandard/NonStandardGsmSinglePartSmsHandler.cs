using System.Collections.Generic;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers.NonStandard
{
    public class NonStandardGsmSinglePartSmsHandler : 
        SingleSmsPartHandlerBase, INonStandardGsmSinglePartSmsHandler
    {
        public SmsPartsInformationDto GetSmsParts(string text)
        {
            return new SmsPartsInformationDto
            {
                HasNonStandardGsmCharacters = true,
                SmsParts = new List<SmsPart>
                {
                    GetSingleSmsPart(text, s => s.Length)
                }
            };
        }
    }
}
