using System;
using FluentAssertions;
using Moq;
using SMSParts.Business.Implementation.Handlers.Standard;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Business.Interfaces.Helpers;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Handlers.Standard
{
    public class StandardGsmSinglePartSmsHandlerShould
    {
        private Mock<IGsmCharactersBytesHelper> HelperMock { get; }
        private IStandardGsmSinglePartSmsHandler StandardGsmSinglePartSmsHandler { get; }

        private readonly string _defaultInput = Guid.NewGuid().ToString();
        private const int DefaultCount = 13;
        public StandardGsmSinglePartSmsHandlerShould()
        {
            HelperMock = new Mock<IGsmCharactersBytesHelper>();
            SetupHelperMock();

            StandardGsmSinglePartSmsHandler = new StandardGsmSinglePartSmsHandler(HelperMock.Object);
        }

        private void SetupHelperMock(int count = DefaultCount)
        {
            HelperMock
                .Setup(helper => helper.GetTextBytesNeeded(It.IsAny<string>()))
                .Returns(count);
        }

        [Fact]
        public void CallGetTextBytesOnce()
        {
            // act
            StandardGsmSinglePartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            HelperMock
                .Verify(helper => helper.GetTextBytesNeeded(_defaultInput),
                    Times.Once);
        }

        [Fact]
        public void ReturnExpectedSmsInformation()
        {
            // act
            var result = StandardGsmSinglePartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            result.HasNonStandardGsmCharacters.Should().BeFalse();
            result.SmsParts.Should().HaveCount(1);
            result.SmsParts.Should()
                .ContainSingle(item 
                    => item.Part == _defaultInput &&
                       item.CharacterBytesCount == DefaultCount);
        }
    }
}
