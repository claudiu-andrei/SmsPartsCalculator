using FluentAssertions;
using SMSParts.Business.Implementation;
using SMSParts.Business.Interfaces;
using SMSParts.Domain.Common;
using Xunit;

namespace SMSParts.Business.Test.Implementation
{
    public class ValidateSmsInputTextShould
    {
        private IValidateSmsInputText ValidateSmsInputText { get; }

        public ValidateSmsInputTextShould()
        {
            ValidateSmsInputText = new ValidateSmsInputText();
        }

        [Fact]
        public void ReturnEmptyMessageIsConditionNotMetForStandardCharacters()
        {
            // arrange
            var charCount = GlobalVariables.SmsParts.MultipleStandardGsm * GlobalVariables.SmsParts.MaxParts;

            // act
            var result = ValidateSmsInputText.Validate(false, charCount);

            // assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ReturnEmptyMessageIsConditionNotMetForNonStandardCharacters()
        {
            // arrange
            var charCount = GlobalVariables.SmsParts.MultipleNonStandardGsm * GlobalVariables.SmsParts.MaxParts;

            // act
            var result = ValidateSmsInputText.Validate(true, charCount);

            // assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ReturnErrorMessageIsConditionMetForStandardCharacters()
        {
            // arrange
            var charCount = GlobalVariables.SmsParts.MultipleStandardGsm * GlobalVariables.SmsParts.MaxParts + 1;

            // act
            var result = ValidateSmsInputText.Validate(false, charCount);

            // assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ReturnErrorMessageIsConditionMetForNonStandardCharacters()
        {
            // arrange
            var charCount = GlobalVariables.SmsParts.MultipleNonStandardGsm * GlobalVariables.SmsParts.MaxParts + 1;

            // act
            var result = ValidateSmsInputText.Validate(true, charCount);

            // assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
