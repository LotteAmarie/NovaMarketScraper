using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace NovaMarketScraper.Core.Data
{
    public class ItemList
    {
        private readonly List<Item> _items;

        public ItemList()
        {
            _items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(@".\Data\items.json"));
        }

        public int Count => _items.Count;

        public object this[int index] { get => _items[index]; }

        public bool Contains(Item item)
        {
            return _items.Contains(item);
        }

        public Item FindItemById(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Item> FindItemByName(string name)
        {
            return _items.FindAll(item => item.Name == name);
        }

        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int IndexOf(Item item)
        {
            return _items.IndexOf(item);
        }

    }
}