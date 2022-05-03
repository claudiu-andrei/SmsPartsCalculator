using System;
using SMSParts.Business.Interfaces;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Models;
using SMSParts.Domain.Models.Response;

namespace SMSParts.Business.Implementation
{
    public class SmsPartsService : ISmsPartsService
    {
        private ITextToSmsPartsHandlerFactory TextToSmsPartsHandlerFactory { get; }

        private ICheckTextCharactersService CheckTextCharactersService { get; }
        private IGsmCharactersBytesHelper Helper { get; }
        private IMessageTypeCalculator MessageTypeCalculator { get; }
        private IValidateSmsInputText ValidateSmsInputText { get; }

        public SmsPartsService(
            ITextToSmsPartsHandlerFactory textToSmsPartsHandlerFactory, 
            ICheckTextCharactersService checkTextCharactersService, 
            IGsmCharactersBytesHelper helper, 
            IMessageTypeCalculator messageTypeCalculator, 
            IValidateSmsInputText validateSmsInputText)
        {
            TextToSmsPartsHandlerFactory = textToSmsPartsHandlerFactory;
            CheckTextCharactersService = checkTextCharactersService;
            Helper = helper;
            MessageTypeCalculator = messageTypeCalculator;
            ValidateSmsInputText = validateSmsInputText;
        }

        public Response<SmsPartsInformationDto> GetSmsPartsInformation(string text)
        {
            try
            {
                var hasNonStandardCharacters = CheckTextCharactersService                    
                    .TextContainsNonStandardOrExtendedGsmCharacters(text);
                var totalCharactersNeeded = Helper.GetTextBytesNeeded(text);

                var validationResult = ValidateSmsInputText
                    .Validate(hasNonStandardCharacters, totalCharactersNeeded);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    return new Response<SmsPartsInformationDto>
                    {
                        Problem = validationResult,
                    };
                }

                var smsTextType = MessageTypeCalculator
                    .GetSmsTextType(hasNonStandardCharacters, totalCharactersNeeded);
                
                var partsInformation = TextToSmsPartsHandlerFactory
                    .GetSmsParts(text, smsTextType);
                return new Response<SmsPartsInformationDto>
                {
                    Data = partsInformation
                };
            }
            catch (Exception exc)
            {
                return new Response<SmsPartsInformationDto>
                {
                    Problem = exc.Message
                };
            }
        }
    }
}
