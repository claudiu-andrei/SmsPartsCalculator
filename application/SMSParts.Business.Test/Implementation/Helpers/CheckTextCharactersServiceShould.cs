using FluentAssertions;
using SMSParts.Business.Implementation.Helpers;
using SMSParts.Business.Interfaces.Helpers;
using SMSParts.Business.Test.Implementation.Helpers.DataSets;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Helpers
{
    public class CheckTextCharactersServiceShould
    {
        private ICheckTextCharactersService CheckTextCharactersService { get; }

        public CheckTextCharactersServiceShould()
        {
            CheckTextCharactersService = new CheckTextCharactersService();
        }

        [Theory]
        [ClassData(typeof(CheckTextCharactersServiceDataSet))]
        public void ReturnsTrueForNonStandardCharactersFoundAndFalseOtherwise(
            string text, bool expectedResult)
        {
            // act
            var result = CheckTextCharactersService
                .TextContainsNonStandardOrExtendedGsmCharacters(text);

            // assert
             result.Should().Be(expectedResult);
        }
    }
}
