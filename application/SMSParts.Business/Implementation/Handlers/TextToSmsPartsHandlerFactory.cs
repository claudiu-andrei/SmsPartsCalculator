using System;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Domain.Models;
using static SMSParts.Domain.Common.Enums;

namespace SMSParts.Business.Implementation.Handlers
{
    public class TextToSmsPartsHandlerFactory : ITextToSmsPartsHandlerFactory
    {
        private IStandardGsmSinglePartSmsHandler StandardGsmSingleSmsPartHandler { get; }
        private IStandardGsmMultiPartSmsHandler StandardGsmMultiSmsPartHandler { get; }

        private INonStandardGsmSinglePartSmsHandler NonStandardGsmSinglePartSmsHandler { get; }
        private INonStandardGsmMultiPartSmsHandler NonStandardGsmMultiPartSmsHandler { get; }
        
        public TextToSmsPartsHandlerFactory(
            IStandardGsmSinglePartSmsHandler standardGsmSingleSmsPartHandler,
            IStandardGsmMultiPartSmsHandler standardGsmMultiSmsPartHandler,  
            INonStandardGsmSinglePartSmsHandler nonStandardGsmSinglePartSmsHandler,
            INonStandardGsmMultiPartSmsHandler nonStandardGsmMultiPartSmsHandler)
        {
            StandardGsmSingleSmsPartHandler = standardGsmSingleSmsPartHandler;
            StandardGsmMultiSmsPartHandler = standardGsmMultiSmsPartHandler;
            NonStandardGsmSinglePartSmsHandler = nonStandardGsmSinglePartSmsHandler;
            NonStandardGsmMultiPartSmsHandler = nonStandardGsmMultiPartSmsHandler;
        }
        
        //TODO: Add method for getting first ISmsPartsHandler from list
        public SmsPartsInformationDto GetSmsParts(string text, SmsTextType messageTextType)
        {
            switch (messageTextType)
            {
                case SmsTextType.StandardGsmSinglePartMessage:
                    return StandardGsmSingleSmsPartHandler.GetSmsParts(text);

                case SmsTextType.StandardGsmMultiPartMessage:
                    return StandardGsmMultiSmsPartHandler.GetSmsParts(text);

                case SmsTextType.NonStandardGsmSinglePartMessage:
                    return NonStandardGsmSinglePartSmsHandler.GetSmsParts(text);

                case SmsTextType.NonStandardGsmMultiPartMessage:
                    return NonStandardGsmMultiPartSmsHandler.GetSmsParts(text);

                case SmsTextType.Unknown:
                    throw new Exception("unknown message type.");

                default:
                    return null;
            }
        }
    }
}
