using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SMSParts.Business.Implementation.Handlers;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Domain.Models;
using Xunit;
using static SMSParts.Domain.Common.Enums;

namespace SMSParts.Business.Test.Implementation.Handlers
{
    public class TextToSmsPartsHandlerFactoryShould
    {
        private Mock<IStandardGsmSinglePartSmsHandler> StandardGsmSingleSmsPartHandlerMock { get; }
        private Mock<IStandardGsmMultiPartSmsHandler> StandardGsmMultiSmsPartHandlerMock { get; }

        private Mock<INonStandardGsmSinglePartSmsHandler> NonStandardGsmSinglePartSmsHandlerMock { get; }
        private Mock<INonStandardGsmMultiPartSmsHandler> NonStandardGsmMultiPartSmsHandlerMock { get; }
        
        private ITextToSmsPartsHandlerFactory TextToSmsPartsHandlerFactory { get; }

        private readonly string _defaultInput = Guid.NewGuid().ToString();
        private readonly SmsPartsInformationDto _defaultResponse = new SmsPartsInformationDto
        {
            SmsParts = new List<SmsPart>
            {
                new SmsPart
                {
                    Part = Guid.NewGuid().ToString()
                }
            }
        };

        public TextToSmsPartsHandlerFactoryShould()
        {
            StandardGsmSingleSmsPartHandlerMock = new Mock<IStandardGsmSinglePartSmsHandler>();
            StandardGsmMultiSmsPartHandlerMock = new Mock<IStandardGsmMultiPartSmsHandler>();
            NonStandardGsmSinglePartSmsHandlerMock = new Mock<INonStandardGsmSinglePartSmsHandler>();
            NonStandardGsmMultiPartSmsHandlerMock = new Mock<INonStandardGsmMultiPartSmsHandler>();
            
            SetupStandardGsmSingleSmsPartHandlerMock();
            SetupStandardGsmMultiSmsPartHandlerMock();
            SetupNonStandardGsmSinglePartSmsHandlerMock();
            SetupNonStandardGsmMultiPartSmsHandlerMock();

            TextToSmsPartsHandlerFactory = new TextToSmsPartsHandlerFactory(
                StandardGsmSingleSmsPartHandlerMock.Object,
                StandardGsmMultiSmsPartHandlerMock.Object,
                NonStandardGsmSinglePartSmsHandlerMock.Object,
                NonStandardGsmMultiPartSmsHandlerMock.Object);
        }

        private void SetupStandardGsmSingleSmsPartHandlerMock()
        {
            StandardGsmSingleSmsPartHandlerMock
                .Setup(handler => handler.GetSmsParts(It.IsAny<string>()))
                .Returns(_defaultResponse);
        }

        private void SetupStandardGsmMultiSmsPartHandlerMock()
        {
            StandardGsmMultiSmsPartHandlerMock
                .Setup(handler => handler.GetSmsParts(It.IsAny<string>()))
                .Returns(_defaultResponse);
        }

        private void SetupNonStandardGsmSinglePartSmsHandlerMock()
        {
            NonStandardGsmSinglePartSmsHandlerMock
                .Setup(handler => handler.GetSmsParts(It.IsAny<string>()))
                .Returns(_defaultResponse);
        }

        private void SetupNonStandardGsmMultiPartSmsHandlerMock()
        {
            NonStandardGsmMultiPartSmsHandlerMock
                .Setup(handler => handler.GetSmsParts(It.IsAny<string>()))
                .Returns(_defaultResponse);
        }
        
        [Fact]
        public void CallStandardGsmMultiSmsPartHandlerForTypeStandardGsmMultiPartMessage()
        {
            // act
            TextToSmsPartsHandlerFactory
                .GetSmsParts(_defaultInput, SmsTextType.StandardGsmMultiPartMessage);

            // assert
            StandardGsmMultiSmsPartHandlerMock
                .Verify(handler => handler.GetSmsParts(_defaultInput),
                    Times.Once);
        }

        [Fact]
        public void CallNonStandardGsmSinglePartSmsHandlerForTypeNonStandardGsmSinglePartMessage()
        {
            // act
            TextToSmsPartsHandlerFactory
                .GetSmsParts(_defaultInput, SmsTextType.NonStandardGsmSinglePartMessage);

            // assert
            NonStandardGsmSinglePartSmsHandlerMock
                .Verify(handler => handler.GetSmsParts(_defaultInput),
                    Times.Once);
        }

        [Fact]
        public void CallNonStandardGsmSinglePartSmsHandlerForTypeNonStandardGsmMultiPartMessage()
        {
            // act
            TextToSmsPartsHandlerFactory
                .GetSmsParts(_defaultInput, SmsTextType.NonStandardGsmMultiPartMessage);

            // assert
            NonStandardGsmMultiPartSmsHandlerMock
                .Verify(handler => handler.GetSmsParts(_defaultInput),
                    Times.Once);
        }

        [Fact]
        public void ThrowExceptionWhenReceivingUnknownSmsTextType()
        {
            // act
            Action act = () => TextToSmsPartsHandlerFactory
                .GetSmsParts(_defaultInput, SmsTextType.Unknown);

            // assert
            act.Should().Throw<Exception>();
        }

    }
}
