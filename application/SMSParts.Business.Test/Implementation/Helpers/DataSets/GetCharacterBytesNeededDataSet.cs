using System.Collections;
using System.Collections.Generic;

namespace SMSParts.Business.Test.Implementation.Helpers.DataSets
{
    public class GetCharacterBytesNeededDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                'a',
                1
            };

            yield return new object[]
            {
                '^',
                2
            };

            yield return new object[]
            {
                'ń',
                1
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
