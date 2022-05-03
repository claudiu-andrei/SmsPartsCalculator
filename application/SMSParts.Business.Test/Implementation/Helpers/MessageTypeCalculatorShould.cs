using FluentAssertions;
using SMSParts.Business.Implementation.Helpers;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Helpers
{
    public class MessageTypeCalculatorShould
    {
        private IMessageTypeCalculator MessageTypeCalculator { get; }

        public MessageTypeCalculatorShould()
        {
            MessageTypeCalculator = new MessageTypeCalculator();
        }

        [Theory]
        [InlineData(false, 3, Enums.SmsTextType.StandardGsmSinglePartMessage)]
        [InlineData(false, 160, Enums.SmsTextType.StandardGsmSinglePartMessage)]
        [InlineData(false, 161, Enums.SmsTextType.StandardGsmMultiPartMessage)]
        [InlineData(false, 345, Enums.SmsTextType.StandardGsmMultiPartMessage)]
        [InlineData(true, 3, Enums.SmsTextType.NonStandardGsmSinglePartMessage)]
        [InlineData(true, 70, Enums.SmsTextType.NonStandardGsmSinglePartMessage)]
        [InlineData(true, 71, Enums.SmsTextType.NonStandardGsmMultiPartMessage)]
        [InlineData(true, 323, Enums.SmsTextType.NonStandardGsmMultiPartMessage)]
        public void ReturnsExpectedSmsTextTypeEnumValue(
            bool hasNonStandardCharacters,
            int totalCharactersNeeded,
            Enums.SmsTextType expectedType)
        {
            // act
            var resultType = MessageTypeCalculator
                .GetSmsTextType(hasNonStandardCharacters, totalCharactersNeeded);

            // assert
            resultType.Should().Be(expectedType);
        }
    }
}
