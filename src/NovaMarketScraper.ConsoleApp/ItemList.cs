using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace NovaMarketScraper.ConsoleApp
{
    public class ItemList : IEnumerable<Item>
    {
        public IEnumerator<Item> GetEnumerator()
        {
            Item item = null;

            do
            {
                yield return item;
                Thread.Sleep(500);
            } while (item != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}