using System;
using System.Collections;
using System.Collections.Generic;
using Planets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Planets
{
    public class InventorySystem : MonoBehaviour
    {
        public SlotController hoverSlotController;
        public EventHandler<List<Slot>> InventoryUpdated { get; set; }
        public List<Slot> Slots { get; set; }

        private ItemDatabase _itemDatabase;

        public Slot HoverSlot
        {
            get;
            set;
        }

        private IDictionary<Item, int> _inventory = new Dictionary<Item, int>();
        private int _pickedUpSlotIndex = 0;

        public void Start()
        {
            _itemDatabase = GetComponent<ItemDatabase>();


            //
            // _slots[0].Item = _itemDatabase.GetItem(0);
            // _slots[0].Count = 50;
            //
            // _slots[2].Item = _itemDatabase.GetItem(0);
            // _slots[2].Count = 5;

            PickUp(new Slot { Item = _itemDatabase.GetItem(0), Count = 10 });

            HoverSlot = hoverSlotController.Slot;
            HoverSlot.Item = null;
            HoverSlot.Count = 0;
        }

        public void UpdateSlots()
        {
            Slots = new List<Slot>();

            foreach (var item in _inventory)
            {
                Slots.Add(new Slot { Item = item.Key, Count = item.Value });
            }
        }

        public void PickUp(Slot slot)
        {
            if (_inventory.ContainsKey(slot.Item))
            {
                _inventory[slot.Item] += slot.Count;
            }
            else
            {
                _inventory.Add(slot.Item, slot.Count);
            }

            UpdateSlots();
            InventoryUpdated?.Invoke(this, Slots);
        }

        public void Drop(Slot slot)
        {
            _inventory[slot.Item] -= slot.Count;
            InventoryUpdated?.Invoke(this, Slots);
        }

        public void TakeToHoverSlot(Slot slot)
        {
            HoverSlot.Set(slot.Item, slot.Count);
            //_pickedUpSlotIndex
        }
    }
}
