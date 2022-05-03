using System.Collections;
using System.Collections.Generic;

namespace SMSParts.Business.Test.Implementation.Helpers.DataSets
{
    public class GetTextBytesNeededDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "test",
                4
            };

            yield return new object[]
            {
                "test^",
                6
            };

            yield return new object[]
            {
                "test😛",
                6
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
