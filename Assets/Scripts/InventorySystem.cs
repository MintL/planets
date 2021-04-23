using System;
using System.Collections;
using System.Collections.Generic;
using Planets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Planets
{
    public class InventorySystem : MonoBehaviour
    {
        [FormerlySerializedAs("hoverSlotController")] public SlotController handSlotController;
        public EventHandler<List<Slot>> InventoryUpdated { get; set; }
        public List<Slot> Slots { get; set; }

        private ItemDatabase _itemDatabase;

        public Slot HandSlot
        {
            get;
            set;
        }

        private IDictionary<Item, int> _inventory = new Dictionary<Item, int>();
        private int _handSlotIndex = -1;

        private void Awake()
        {
            Slots ??= new List<Slot>();
            for (int i = 0; i < 39; i++)
            {
                Slots.Add(new Slot());
            }
        }

        public void Start()
        {
            _itemDatabase = GetComponent<ItemDatabase>();

            _inventory.Add(_itemDatabase.GetItem(0), 50);
            _inventory.Add(_itemDatabase.GetItem(1), 5);

            HandSlot = new Slot();
            handSlotController.OnSlotChanged(HandSlot);
        }

        public void UpdateSlots()
        {
            var i = 0;
            foreach (var item in _inventory)
            {
                if (_handSlotIndex == i)
                {
                    HandSlot.Set(item.Key, item.Value);
                    Slots[i].Set(null, 0);
                }
                else
                {
                    Slots[i].Set(item.Key, item.Value);
                }
                i++;
            }
        }

        public void PickUp(Item item, int count)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item] += count;
            }
            else
            {
                _inventory.Add(item, count);
            }

            UpdateSlots();
        }

        public void Drop(Item item, int count)
        {
            if (_inventory.ContainsKey(item))
            {
                var minCount = Math.Min(_inventory[item], count);
                if (minCount == count)
                {
                    _inventory.Remove(item);
                }
                else
                {
                    _inventory[item] -= minCount;
                }
            }
        }

        public void UpdateHandSlot(Slot slot)
        {
            _handSlotIndex = slot.Item == null ? -1 : Slots.IndexOf(slot);
            UpdateSlots();
        }
    }
}
