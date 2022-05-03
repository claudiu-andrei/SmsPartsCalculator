using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SMSParts.Business.Implementation.Handlers;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Test.Implementation.Handlers.DataSets;
using SMSParts.Domain.Common;
using Xunit;

namespace SMSParts.Business.Test.Implementation.Handlers
{
    public class SplitSmsServiceShould
    {
        private ISplitSmsService SplitSmsService { get; }

        public SplitSmsServiceShould()
        {
            SplitSmsService = new SplitSmsService();
        }

        private readonly Func<char, int> _defaultFunc = 
            (c => GlobalVariables.GsmCharacters.ExtendedCharacters
                .Contains(c) ? 2 : 1);

        [Theory]
        [ClassData(typeof(SplitSmsServiceDataSet))]
        public void ReturnTheExpectedList(
            string text, 
            int maxSize, 
            IEnumerable<IEnumerable<char>> expectedResult)
        {
            // act
            var result = SplitSmsService.Split(text, maxSize, _defaultFunc);

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
