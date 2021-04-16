using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Planets
{
    public class InventoryController : MonoBehaviour, IPanel
    {
        private InventorySystem _inventory;
        private List<SlotController> _slots;

        private RectTransform _canvas;
        private CanvasScaler _scaler;

        public void Start()
        {
            _canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            _scaler = _canvas.GetComponent<CanvasScaler>();

            _slots = new List<SlotController>();
            foreach (Transform child in transform.Find("Slots").transform)
            {
                var slotController = child.GetComponent<SlotController>();
                _slots.Add(slotController);
                slotController.Clicked += OnSlotClicked;
            }

            _inventory = FindObjectOfType<InventorySystem>();
            _inventory.InventoryUpdated += OnInventoryUpdated;
        }

        public void OnShow()
        {
            UpdateSlots(_inventory.Slots);
        }

        public void OnElementChanged(object element)
        {

        }

        public void Update()
        {
            if (_inventory.HoverSlot != null)
            {
                var mousePos = Mouse.current.position.ReadValue();
                var referenceRel = new Vector2(_scaler.referenceResolution.x / Screen.width,
                    _scaler.referenceResolution.y / Screen.height);
                var pos = new Vector2(mousePos.x, mousePos.y) * referenceRel -
                          _scaler.referenceResolution * 0.5f;
                _inventory.hoverSlotController.transform.localPosition = pos + new Vector2(5, -5);
            }
        }

        public void OnSlotClicked(object sender, PointerEventData eventData)
        {
            if (sender is Slot slot)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    if (_inventory.HoverSlot != null)
                    {
                        slot.TransferTo(_inventory.HoverSlot);
                    }
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    //_inventory.HoverSlot.TransferTo(slot, 1);
                }
            }
        }

        private void OnInventoryUpdated(object sender, List<Slot> slots)
        {
            UpdateSlots(slots);
        }

        private void UpdateSlots(List<Slot> slots)
        {
            for (var i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                var controller = _slots[i];

                if (slot != null)
                {
                    controller.Slot.Set(slot.Item, slot.Count);
                }
            }
        }
    }
}
