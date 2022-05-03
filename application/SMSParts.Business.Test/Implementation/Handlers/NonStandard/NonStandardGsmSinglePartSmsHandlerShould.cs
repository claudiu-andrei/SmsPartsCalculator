using System;
using FluentAssertions;
using SMSParts.Business.Implementation.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Handlers.NonStandard
{
    public class NonStandardGsmSinglePartSmsHandlerShould
    {
        private INonStandardGsmSinglePartSmsHandler NonStandardGsmSinglePartSmsHandler { get; }

        private readonly string _defaultInput = Guid.NewGuid().ToString();

        public NonStandardGsmSinglePartSmsHandlerShould()
        {
            NonStandardGsmSinglePartSmsHandler = new NonStandardGsmSinglePartSmsHandler();
        }
        
        [Fact]
        public void ReturnExpectedSmsInformation()
        {
            // act
            var result = NonStandardGsmSinglePartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            result.HasNonStandardGsmCharacters.Should().BeTrue();
            result.SmsParts.Should().HaveCount(1);
            result.SmsParts.Should()
                .ContainSingle(item
                    => item.Part == _defaultInput &&
                       item.CharacterBytesCount == _defaultInput.Length);
        }
    }
}
