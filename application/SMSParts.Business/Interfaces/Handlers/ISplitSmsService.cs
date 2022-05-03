using System;
using System.Collections.Generic;

namespace SMSParts.Business.Interfaces.Handlers
{
    public interface ISplitSmsService
    {
        IEnumerable<IEnumerable<T>> Split<T>(
            IEnumerable<T> source,
            int partMaxSize,
            Func<T, int> itemSizeFunc);
    }
}
