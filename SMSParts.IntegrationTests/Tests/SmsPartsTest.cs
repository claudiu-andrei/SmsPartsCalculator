using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
