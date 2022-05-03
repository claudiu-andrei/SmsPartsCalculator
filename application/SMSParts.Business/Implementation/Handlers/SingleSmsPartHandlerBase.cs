using System;
using SMSParts.Domain.Models;

namespace SMSParts.Business.Implementation.Handlers
{
    public class SingleSmsPartHandlerBase
    {
        protected SmsPart GetSingleSmsPart(string item, Func<string, int> getBytes)
        {
            return new SmsPart
            {
                Part = item,
                CharacterBytesCount = getBytes(item)
            };
        }

        
    }
}
