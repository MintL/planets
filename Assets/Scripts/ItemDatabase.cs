using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Planets
{
    public class ItemDatabase : MonoBehaviour
    {
        private IDictionary<int, Item> _items;

        public void Awake()
        {
            BuildDatabase();
        }

        public Item GetItem(int id) => _items[id];

        public Item GetItem(string name) => _items.Values.FirstOrDefault(i => i.Name == name);

        private void BuildDatabase()
        {
            _items = new Dictionary<int, Item>
            {
                { 0, new Item(0, "Wood", "wood") },
                { 1, new Item(1, "Iron Ore", "iron-ore") },
                { 2, new Item(2, "Iron Ingot", "iron-ingot") },
            };
        }
    }
}
