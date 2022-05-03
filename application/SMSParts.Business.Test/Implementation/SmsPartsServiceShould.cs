using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SMSParts.Business.Implementation;
using SMSParts.Business.Interfaces;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Models;
using SMSParts.Domain.Models.Response;
using Xunit;
using static SMSParts.Domain.Common.Enums;

namespace SMSParts.Business.Test.Implementation
{
    public class SmsPartsServiceShould
    {
        private Mock<ITextToSmsPartsHandlerFactory> TextToSmsPartsHandlerFactoryMock { get; }
        private Mock<ICheckTextCharactersService> CheckTextCharactersServiceMock { get; }
        private Mock<IGsmCharactersBytesHelper> GsmCharacterBytesHelperMock { get; }
        private Mock<IMessageTypeCalculator> MessageTypeCalculatorMock { get; }
        private Mock<IValidateSmsInputText> ValidateSmsInputTextMock { get; }

        private ISmsPartsService SmsPartsService { get; }

        private readonly bool _defaultHasNonStandardCharacters = false;
        private readonly int _defaultCharacterCount = 100;
        private readonly string _defaultInputText = Guid.NewGuid().ToString();
        private readonly SmsTextType _defaultInputType = SmsTextType.StandardGsmSinglePartMessage;
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

        private readonly Exception _defaultFactoryException = new Exception(Guid.NewGuid().ToString());

        public SmsPartsServiceShould()
        {
            TextToSmsPartsHandlerFactoryMock = new Mock<ITextToSmsPartsHandlerFactory>();
            CheckTextCharactersServiceMock = new Mock<ICheckTextCharactersService>();
            GsmCharacterBytesHelperMock = new Mock<IGsmCharactersBytesHelper>();
            MessageTypeCalculatorMock = new Mock<IMessageTypeCalculator>();
            ValidateSmsInputTextMock = new Mock<IValidateSmsInputText>();

            SetupCheckTextCharactersServiceMock(_defaultHasNonStandardCharacters);
            SetupGsmCharacterBytesHelperMock(_defaultCharacterCount);
            SetupValidateSmsInputTextMock(string.Empty);
            SetupMessageTypeCalculatorMock(_defaultInputType);
            SetupTextToSmsPartsHandlerFactoryMock();

            SmsPartsService = new SmsPartsService(
                TextToSmsPartsHandlerFactoryMock.Object,
                CheckTextCharactersServiceMock.Object,
                GsmCharacterBytesHelperMock.Object,
                MessageTypeCalculatorMock.Object,
                ValidateSmsInputTextMock.Object);
        }

        private void SetupCheckTextCharactersServiceMock(bool hasNonStandardCharacters)
        {
            CheckTextCharactersServiceMock
                .Setup(service => service
                    .TextContainsNonStandardOrExtendedGsmCharacters(It.IsAny<string>()))
                .Returns(hasNonStandardCharacters);
        }

        private void SetupGsmCharacterBytesHelperMock(int charCount)
        {
            GsmCharacterBytesHelperMock
                .Setup(service => service
                    .GetTextBytesNeeded(It.IsAny<string>()))
                .Returns(charCount);
        }

        private void SetupValidateSmsInputTextMock(string validationResult)
        {
            ValidateSmsInputTextMock
                .Setup(service => service.Validate(It.IsAny<bool>(), It.IsAny<int>()))
                .Returns(validationResult);
        }

        private void SetupMessageTypeCalculatorMock(SmsTextType type)
        {
            MessageTypeCalculatorMock
                .Setup(calculator => calculator.GetSmsTextType(It.IsAny<bool>(), It.IsAny<int>()))
                .Returns(type);
        }

        private void SetupTextToSmsPartsHandlerFactoryMock(bool throwsException = false)
        {
            var setup = TextToSmsPartsHandlerFactoryMock
                .Setup(mock => mock
                    .GetSmsParts(It.IsAny<string>(), It.IsAny<SmsTextType>()));

            if (!throwsException)
            {
                setup.Returns(_defaultResponse);
                return;
            }

            setup.Throws(_defaultFactoryException);
        }

        [Fact]
        public void CallCheckTextCharactersServiceOnce()
        {
            // act
            SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            CheckTextCharactersServiceMock
                .Verify(mock => mock.TextContainsNonStandardOrExtendedGsmCharacters(_defaultInputText),
                    Times.Once);
        }

        [Fact]
        public void CallGsmCharacterBytesHelperMockOnce()
        {
            // act
            SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            GsmCharacterBytesHelperMock
                .Verify(mock => mock.GetTextBytesNeeded(_defaultInputText),
                    Times.Once);
        }

        [Fact]
        public void CallValidateOnceWithExpectedParameters()
        {
            // act
            SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            ValidateSmsInputTextMock
                .Verify(mock => mock
                        .Validate(_defaultHasNonStandardCharacters, _defaultCharacterCount),
                    Times.Once);
        }

        [Fact]
        public void ReturnsValidationResultIfNotNullOrEmpty()
        {
            // arrange
            var error = "some error";
            SetupValidateSmsInputTextMock(error);

            // act
            var result = SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            result.IsValid.Should().BeFalse();
            result.Problem.Should().BeEquivalentTo(error);
        }

        [Fact]
        public void CallMessageTypeCalculatorMockOnceWithExpectedParameters()
        {
            // act
            SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            MessageTypeCalculatorMock
                .Verify(mock => mock.GetSmsTextType(_defaultHasNonStandardCharacters, _defaultCharacterCount),
                    Times.Once);
        }

        [Fact]
        public void CallTextToSmsPartsHandlerFactoryOnceWithExpectedInput()
        {
            // act
            SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            TextToSmsPartsHandlerFactoryMock
                .Verify(mock => mock.GetSmsParts(_defaultInputText, _defaultInputType), 
                Times.Once);
        }

        [Fact]
        public void ReturnExpectedResponseFromFactory()
        {
            // act
            var result = SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            result.Data.Should().BeEquivalentTo(_defaultResponse);
        }

        [Fact]
        public void ReturnExpectedExceptionResponseFromFactory()
        {
            // arrange
            SetupTextToSmsPartsHandlerFactoryMock(true);

            // act
            var result = SmsPartsService.GetSmsPartsInformation(_defaultInputText);

            // assert
            result.Should()
                .Match<Response<SmsPartsInformationDto>>(resp => 
                !resp.IsValid && resp.Problem == _defaultFactoryException.Message);
        }
    }
}
