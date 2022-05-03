using System;
using System.Collections.Generic;
using SMSParts.Business.Interfaces.Handlers;

namespace SMSParts.Business.Implementation.Handlers
{
    public class SplitSmsService : ISplitSmsService
    {
        public IEnumerable<IEnumerable<T>> Split<T>(
            IEnumerable<T> source,
            int partMaxSize,
            Func<T, int> itemSizeFunc)
        {
            var items = new List<T>();
            var currentSize = 0;
            foreach (var item in source)
            {
                var itemSize = itemSizeFunc(item);
                if (Fits(currentSize, itemSize, partMaxSize))
                {
                    items.Add(item);
                }
                else
                {
                    yield return items.ToArray();
                    items.Clear();
                    currentSize = 0;
                    items.Add(item);
                }

                currentSize += itemSize;
            }

            if (items.Count > 0)
            {
                yield return items.ToArray();
            }
        }

        private static bool Fits(int currentSize, int itemSize, int partMaxSize)
        {
            return currentSize + itemSize <= partMaxSize;
        }
    }
}
