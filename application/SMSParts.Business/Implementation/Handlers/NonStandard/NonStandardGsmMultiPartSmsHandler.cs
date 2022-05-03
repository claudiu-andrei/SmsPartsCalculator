using System.Linq;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Domain.Common;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers.NonStandard
{
    public class NonStandardGsmMultiPartSmsHandler : SmsPartHandlerBase, INonStandardGsmMultiPartSmsHandler
    {
        private ISplitSmsService SplitSmsService { get; }

        public NonStandardGsmMultiPartSmsHandler(ISplitSmsService splitSmsService) 
            : base(GlobalVariables.SmsParts.MultipleNonStandardGsm)
        {
            SplitSmsService = splitSmsService;
        }

        ////TODO: Decide whether to use similar
        ////logic to: https://messente.com/documentation/tools/sms-length-calculator
        public SmsPartsInformationDto GetSmsParts(string text)
        {   
            var parts = SplitSmsService.Split(text, MaxAllowedChars, c => 1);

            var result = new SmsPartsInformationDto
            {
                HasNonStandardGsmCharacters = true,
                SmsParts = parts
                    .Select(item => GetSmsPart(item, s => s.Length))
                    .ToList()
            };

            return result;
        }
    }
}
