using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Planets
{
    public class MiningDrillController : MonoBehaviour, IPanel
    {
        private Image _loading;
        private Slot _outputSlot;
        private MiningDrill _miningDrill;

        private void Start()
        {
            _loading = transform.Find("Output").Find("Loading").Find("Fill").GetComponent<Image>();
            _loading.fillAmount = 0;

            var slotController = transform.Find("Output").Find("Slot").GetComponent<SlotController>();
            _outputSlot = slotController.Slot;
            slotController.Clicked += OnOutputClicked;
        }

        public void OnShow()
        {

        }

        public void OnElementChanged(object element)
        {
            if (element is MiningDrill drill)
            {
                _miningDrill = drill;
                _miningDrill.PercentageUpdated += OnPercentageUpdated;
                _miningDrill.SlotUpdated += OnSlotUpdated;

                UpdateLoading(_miningDrill.Percentage);
                UpdateSlot(_miningDrill.Slot);
            }
            else if (_miningDrill != null)
            {
                _miningDrill.PercentageUpdated -= OnPercentageUpdated;
                _miningDrill.SlotUpdated -= OnSlotUpdated;
                _miningDrill = null;
            }
        }

        private void OnOutputClicked(object sender, PointerEventData e)
        {
            if (sender is Slot slot)
            {
                _miningDrill.PickUp(slot);
            }
        }

        private void OnPercentageUpdated(object sender, float percentage)
        {
            UpdateLoading(percentage);
        }

        private void OnSlotUpdated(object sender, Slot slot)
        {
            UpdateSlot(slot);
        }

        private void UpdateLoading(float percentage)
        {
            _loading.fillAmount = percentage;
        }

        private void UpdateSlot(Slot slot)
        {
            _outputSlot.Set(slot.Item, slot.Count);
        }

    }
}
