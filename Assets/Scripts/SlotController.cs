using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Planets
{
    public class SlotController : MonoBehaviour, IPointerClickHandler
    {
        private Image _image;
        private Text _textCount;
        private InventorySystem _inventory;

        public EventHandler<PointerEventData> Clicked;

        public Slot Slot { get; private set; }

        void Awake()
        {
            _image = transform.Find("Image").GetComponent<Image>();
            _textCount = transform.Find("Count").GetComponent<Text>();
        }

        public void OnSlotChanged(Slot slot)
        {
            if (Slot != null)
            {
                Slot.CountChanged = null;
                Slot.ItemChanged = null;
            }

            Slot = slot;
            Slot.CountChanged = OnCountChanged;
            Slot.ItemChanged = OnItemChanged;
            OnCountChanged(this, Slot.Count);
            OnItemChanged(this, Slot.Item);
        }

        private void Start()
        {
            _inventory = FindObjectOfType<InventorySystem>();
        }

        private void OnCountChanged(object sender, int count)
        {
            if (count > 1)
            {
                _textCount.enabled = true;
                _textCount.text = count.ToString();
            }
            else
            {
                _textCount.text = "";
                _textCount.enabled = false;
            }
            _image.enabled = count > 0;
        }

        private void OnItemChanged(object sender, Item item)
        {
            if (item != null)
            {
                _image.sprite = item.Sprite;
            }
            else
            {
                _image.enabled = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Clicked?.Invoke(Slot, eventData);

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _inventory.UpdateHandSlot(Slot);
                // else if (Slot.Item == null)
                // {
                //     _inventory.PickUp(_inventory.HoverSlot);
                //     _inventory.HoverSlot.Item = null;
                //     _inventory.HoverSlot.Count = 0;
                // }
                // else if (Slot.Item != null)
                // {
                //     var oldItem = Slot.Item;
                // }
            }
        }
    }
}
