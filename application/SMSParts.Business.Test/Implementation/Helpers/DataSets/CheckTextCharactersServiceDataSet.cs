using System.Collections;
using System.Collections.Generic;

namespace SMSParts.Business.Test.Implementation.Helpers.DataSets
{
    public class CheckTextCharactersServiceDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                " ",
                false
            };

            yield return new object[]
            {
                "test", 
                false
            };

            yield return new object[]
            {
                "123!@ #asd",
                false
            };

            yield return new object[]
            {
                "test with extended^character",
                false
            };

            yield return new object[]
            {
                "non standard: ń (polish wymysorys)",
                true
            };

            yield return new object[]
            {
                "test some characters and 🍕 emoji",
                true
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
