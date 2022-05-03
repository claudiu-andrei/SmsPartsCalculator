using System;
using System.Collections.Generic;
using System.Linq;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers
{
    public class SmsPartHandlerBase : SingleSmsPartHandlerBase
    {
        protected readonly int MaxAllowedChars;

        protected SmsPartHandlerBase(int maxAllowedChars)
        {
            MaxAllowedChars = maxAllowedChars;
        }

        protected SmsPart GetSmsPart(IEnumerable<char> item, 
            Func<string, int> getBytes)
        {
            var itemString = new string(item.ToArray());

            return GetSingleSmsPart(itemString, getBytes);
        }
    }
}
