using System.Linq;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers.Standard
{
    public class StandardGsmMultiPartSmsHandler : 
        SmsPartHandlerBase, IStandardGsmMultiPartSmsHandler
    {
        private IGsmCharactersBytesHelper Helper { get; }
        private ISplitSmsService SplitSmsService { get; }

        public StandardGsmMultiPartSmsHandler(
            IGsmCharactersBytesHelper helper, 
            ISplitSmsService splitSmsService) 
            : base(GlobalVariables.SmsParts.MultipleStandardGsm)
        {
            Helper = helper;
            SplitSmsService = splitSmsService;
        }

        public SmsPartsInformationDto GetSmsParts(string text)
        {
            var parts = SplitSmsService.Split(text, MaxAllowedChars, Helper.GetCharacterBytesNeeded);

            var result = new SmsPartsInformationDto
            {
                HasNonStandardGsmCharacters = false,
                SmsParts = parts
                    .Select(item => GetSmsPart(item, Helper.GetTextBytesNeeded))
                    .ToList()
            };

            return result;
        }
    }
}
