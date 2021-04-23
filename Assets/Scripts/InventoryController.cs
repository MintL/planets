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

            _inventory = FindObjectOfType<InventorySystem>();
            _inventory.InventoryUpdated += OnInventoryUpdated;

            _slots = new List<SlotController>();
            var parent = transform.Find("Slots").transform;
            for (var i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                var slotController = child.GetComponent<SlotController>();
                _slots.Add(slotController);
                slotController.Clicked += OnSlotClicked;
                slotController.OnSlotChanged(_inventory.Slots[i]);
            }
        }

        public void OnShow()
        {
            _inventory.UpdateSlots();
        }

        public void OnElementChanged(object element)
        {

        }

        public void Update()
        {
            if (_inventory.HandSlot != null)
            {
                var mousePos = Mouse.current.position.ReadValue();
                var referenceRel = new Vector2(_scaler.referenceResolution.x / Screen.width,
                    _scaler.referenceResolution.y / Screen.height);
                var pos = new Vector2(mousePos.x, mousePos.y) * referenceRel -
                          _scaler.referenceResolution * 0.5f;
                _inventory.handSlotController.transform.localPosition = pos + new Vector2(5, -5);
            }
        }

        public void OnSlotClicked(object sender, PointerEventData eventData)
        {
            if (sender is Slot slot)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    slot.Count += 10;
                    // if (_inventory.HoverSlot != null)
                    // {
                    //     slot.TransferTo(_inventory.HoverSlot);
                    // }
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    //_inventory.HoverSlot.TransferTo(slot, 1);
                }
            }
        }

        private void OnInventoryUpdated(object sender, List<Slot> slots)
        {
            //UpdateSlots(slots);
        }

        private void UpdateSlots(List<Slot> slots)
        {
            foreach (var slot in slots)
            {

            }
        }
    }
}
