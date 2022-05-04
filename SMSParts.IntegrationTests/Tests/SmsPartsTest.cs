using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using SMSParts.IntegrationTests.Models;
using SMSParts.IntegrationTests.Tests.DataSets;
using Xunit;
using Xunit.Abstractions;

namespace SMSParts.IntegrationTests.Tests
{
    public class SmsPartsTest : IntegrationTestBase
    {
        public SmsPartsTest(SmsPartsFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Theory]
        [ClassData(typeof(SmsPartsTestDataSet))]
        public async Task GetPartsReturnsExpectedSmsPartsInformation(
            string input, 
            SmsPartsInformationDto expectedResult,
            string description)
        {
            // arrange
            Output.WriteLine(description);

            // act
            var smsInfoResult = await ApiHelper.GetPartsWithAssertion(input);

            // assert
            smsInfoResult.Should().BeEquivalentTo(expectedResult, description);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CheckThatExact255PartMessageIsAllowed(
            bool hasNonStandardCharacters)
        {
            // arrange
            var maxAllowedParts = 255;
            var messageSize = hasNonStandardCharacters ? 67 : 153;
            var maxAllowedSize = messageSize * maxAllowedParts;

            var lastCharacter = hasNonStandardCharacters ? 'ń' : 'a';

            var inputText = new string('a', maxAllowedSize - 1) + lastCharacter;

            // act
            var smsInfoResult = await ApiHelper.GetPartsWithAssertion(inputText);

            // assert
            smsInfoResult.SmsPartsCount.Should().Be(maxAllowedParts);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CheckThatForMoreThan255MessagePartsWeReceiveTheAppropriateError(
            bool hasNonStandardCharacters)
        {
            // arrange
            var maxAllowedParts = 255;

            var messageSize = hasNonStandardCharacters ? 67 : 153;
            var maxAllowedSize = messageSize * maxAllowedParts;

            var firstCharacter = hasNonStandardCharacters ? 'ń' : 'a';

            var inputText = firstCharacter  + new string('a', maxAllowedSize);

            // act
            var smsInfoResult = await ApiHelper
                .GetPartsErrorWithStatusCodeAssertion(inputText,
                    HttpStatusCode.InternalServerError);

            // assert
            smsInfoResult.Error.Content.Should()
                .Be("Text size extends beyond allowed max 255 sms parts.");
        }
    }
}
