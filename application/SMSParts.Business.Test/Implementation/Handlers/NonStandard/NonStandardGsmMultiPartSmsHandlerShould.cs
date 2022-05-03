using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using SMSParts.Business.Implementation.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Domain.Common;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Handlers.NonStandard
{
    public class NonStandardGsmMultiPartSmsHandlerShould
    {
        private Mock<ISplitSmsService> SplitSmsServiceMock { get; }

        private readonly string _defaultInput = Guid.NewGuid().ToString();
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

        private INonStandardGsmMultiPartSmsHandler NonStandardGsmMultiPartSmsHandler { get; }

        public NonStandardGsmMultiPartSmsHandlerShould()
        {
            SplitSmsServiceMock = new Mock<ISplitSmsService>();
            SetupSplitSmsServiceMock();

            NonStandardGsmMultiPartSmsHandler = new NonStandardGsmMultiPartSmsHandler(SplitSmsServiceMock.Object);
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
            NonStandardGsmMultiPartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            SplitSmsServiceMock
                .Verify(service => service
                        .Split(
                            _defaultInput,
                            GlobalVariables.SmsParts.MultipleNonStandardGsm,
                            It.IsAny<Func<char, int>>()),
                    Times.Once);
        }

        [Fact]
        public void ReturnExpectedResult()
        {
            // act
            var result = NonStandardGsmMultiPartSmsHandler.GetSmsParts(_defaultInput);

            // assert
            result.HasNonStandardGsmCharacters.Should().BeTrue();
            result.SmsPartsCount.Should().Be(ExpectedParts.Count);
            result.SmsParts.Should().HaveCount(ExpectedParts.Count);
            result.SmsParts.Should()
                .OnlyContain(part => ExpectedParts.Contains(part.Part)
                );
        }
    }
}
