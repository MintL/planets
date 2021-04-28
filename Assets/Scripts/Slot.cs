using System;

namespace Planets
{
    public class Slot
    {
        public EventHandler<Item> ItemChanged;
        public EventHandler<int> CountChanged;

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                ItemChanged?.Invoke(this, value);
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                CountChanged?.Invoke(this, value);
            }
        }

        private Item _item;
        private int _count;

        public void Add(int count)
        {
            Count += count;
        }

        public void Set(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public int Take(int? count = null)
        {
            var max = count == null ? Count : Math.Min(count.Value, Count);
            Count -= max;
            return max;
        }
    }
}
