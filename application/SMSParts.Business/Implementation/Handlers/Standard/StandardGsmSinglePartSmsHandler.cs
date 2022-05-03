using System.Collections.Generic;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers.Standard
{
    public class StandardGsmSinglePartSmsHandler : 
        SingleSmsPartHandlerBase,  IStandardGsmSinglePartSmsHandler
    {
        private IGsmCharactersBytesHelper Helper { get; }

        public StandardGsmSinglePartSmsHandler(IGsmCharactersBytesHelper helper)
        {
            Helper = helper;
        }

        public SmsPartsInformationDto GetSmsParts(string text)
        {
            return new SmsPartsInformationDto
            {
                HasNonStandardGsmCharacters = false,
                SmsParts = new List<SmsPart>
                {
                    GetSingleSmsPart(text, Helper.GetTextBytesNeeded)
                }
            };
        }
    }
}
