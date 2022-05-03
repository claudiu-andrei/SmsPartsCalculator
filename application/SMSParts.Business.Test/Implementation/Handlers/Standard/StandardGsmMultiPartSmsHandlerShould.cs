using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SMSParts.Business.Implementation.Handlers.Standard;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Domain.Common;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Handlers.Standard
{
    public class StandardGsmMultiPartSmsHandlerShould
    {
        private Mock<IGsmCharactersBytesHelper> HelperMock { get; }
        private Mock<ISplitSmsService> SplitSmsServiceMock { get; }

        private IStandardGsmMultiPartSmsHandler StandardGsmMultiPartSmsHandler { get; }

        private readonly string _defaultInput = Guid.NewGuid().ToString();
        private readonly int _defaultCount = 155;

        private static readonly List<string> ExpectedParts = new List<string>
        {
            "test", "otherText"
        };

        private readonly IEnumerable<IEnumerable<char>> _defaultSplitResponse =
            new List<IEnumerable<char>>
            {
                ExpectedParts[0],
                ExpectedParts[1]
            };

        public StandardGsmMultiPartSmsHandlerShould()
        {
            HelperMock = new Mock<IGsmCharactersBytesHelper>();
            SplitSmsServiceMock = new Mock<ISplitSmsService>();

            SetupHelperMock();
            SetupSplitSmsServiceMock();

            StandardGsmMultiPartSmsHandler = new StandardGsmMultiPartSmsHandler(
                HelperMock.Object,
                SplitSmsServiceMock.Object);
        }

        private void SetupHelperMock()
        {
            HelperMock
                .Setup(helper => helper
                    .GetTextBytesNeeded(It.IsAny<string>()))
                .Returns(_defaultCount);
        }

        private void SetupSplitSmsServiceMock()
        {
            SplitSmsServiceMock
                .Setup(service => service
                    .Split(
                        It.IsAny<IEnumerable<char>>(),
                        It.IsAny<int>(),
                        It.IsAny<Func<char, int>>()))
                .Returns(_defaultSplitResponse);
        }

        [Fact]
        public void CallSplitSmsServiceOnce()
        {
            // act
            StandardGsmMultiPartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            SplitSmsServiceMock
                .Verify(service => service
                    .Split(
                        _defaultInput,
                        GlobalVariables.SmsParts.MultipleStandardGsm,
                        It.IsAny<Func<char,int>>()),
                    Times.Once);
        }

        [Fact]
        public void ReturnExpectedResult()
        {
            // act
            var result = StandardGsmMultiPartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            result.HasNonStandardGsmCharacters.Should().BeFalse();
            result.SmsPartsCount.Should().Be(ExpectedParts.Count);
            result.SmsParts.Should().HaveCount(ExpectedParts.Count);
            result.SmsParts.Should()
                .OnlyContain(part
                    => part.CharacterBytesCount == _defaultCount &&
                       ExpectedParts.Contains(part.Part)
                );
        }
    }
}
