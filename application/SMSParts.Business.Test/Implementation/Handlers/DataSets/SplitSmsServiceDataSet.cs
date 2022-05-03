using System.Collections;
using System.Collections.Generic;

namespace SMSParts.Business.Test.Implementation.Handlers.DataSets
{
    public class SplitSmsServiceDataSet : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // standard split
            yield return new object[]
            {
                "a tale of some people coming together",
                7,
                new List<IEnumerable<char>>
                {
                    "a tale ".ToCharArray(),
                    "of some".ToCharArray(),
                    " people".ToCharArray(),
                    " coming".ToCharArray(),
                    " togeth".ToCharArray(),
                    "er".ToCharArray(),
                }
            };

            // split with different character sizes
            yield return new object[]
            {
                "test ń",
                7,
                new List<IEnumerable<char>>
                {
                    "test ń".ToCharArray()
                }
            };

            yield return new object[]
            {
                "test aań",
                7,
                new List<IEnumerable<char>>
                {
                    "test aa".ToCharArray(),
                    "ń".ToCharArray()
                }
            };

            // emoji split between parts
            yield return new object[]
            {
                "test a😛",
                7,
                new List<IEnumerable<char>>
                {
                    ("test a" +'\ud83d').ToCharArray(),
                    '\ude1b'.ToString().ToCharArray()
                }
            };

            yield return new object[]
            {
                "test a😛1234😛😛",
                7,
                new List<IEnumerable<char>>
                {
                    ("test a" +'\ud83d').ToCharArray(),
                    ('\ude1b' + "1234😛").ToCharArray(),
                    "😛".ToCharArray()
                }
            };

            yield return new object[]
            {
                "test a😛123😛😛",
                7,
                new List<IEnumerable<char>>
                {
                    ("test a" +'\ud83d').ToCharArray(),
                    ('\ude1b' + "123😛" + '\ud83d').ToCharArray(),
                    '\ude1b'.ToString().ToCharArray()
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
