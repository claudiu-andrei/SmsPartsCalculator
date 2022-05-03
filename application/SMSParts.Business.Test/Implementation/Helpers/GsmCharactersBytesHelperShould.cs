using FluentAssertions;
using SMSParts.Business.Implementation.Helpers;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Business.Test.Implementation.Helpers.DataSets;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Helpers
{
    public class GsmCharactersBytesHelperShould
    {
        private IGsmCharactersBytesHelper Helper { get; }

        public GsmCharactersBytesHelperShould()
        {
            Helper = new GsmCharactersBytesHelper();
        }

        [Theory]
        [ClassData(typeof(GetTextBytesNeededDataSet))]
        public void GetTextBytesNeedReturnsExpectedCount(string text, int expectedCount)
        {
            // act
            var result = Helper.GetTextBytesNeeded(text);

            // assert
            result.Should().Be(expectedCount);
        }

        [Theory]
        [ClassData(typeof(GetCharacterBytesNeededDataSet))]
        public void GetCharacterBytesNeededReturnExpectedCount(char chr, int expectedCount)
        {
            // act
            var result = Helper.GetCharacterBytesNeeded(chr);

            // assert
            result.Should().Be(expectedCount);
        }
    }
}
