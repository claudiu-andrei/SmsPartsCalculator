using System;
using FluentAssertions;
using SMSParts.Domain.Models.Response;
using Xunit;

namespace SMSParts.Domain.Test.Models.Response
{
    public class ResponseShould
    {
        [Theory]   
        [InlineData("")]
        [InlineData(null)]
        public void HaveIsValidTrueIfProblemIsNullOrEmpty(string problem)
        {
            // arrange
            var response = new Response<string>
            {
                Problem = problem
            };

            // act
            var isValid = response.IsValid;

            // assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void HaveIsValidFalseIfProblemIsNullOrEmpty()
        {
            // arrange
            var response = new Response<string>
            {
                Problem = Guid.NewGuid().ToString()
            };

            // act
            var isValid = response.IsValid;

            // assert
            isValid.Should().BeFalse();
        }
    }
}
