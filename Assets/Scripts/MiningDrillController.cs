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
        private SlotController _slotController;

        private void Start()
        {
            _loading = transform.Find("Output").Find("Loading").Find("Fill").GetComponent<Image>();
            _loading.fillAmount = 0;

            _outputSlot = new Slot();
            _slotController = transform.Find("Output").Find("Slot").GetComponent<SlotController>();
            _slotController.Clicked += OnOutputClicked;
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
                _outputSlot = _miningDrill.Slot;
                _slotController.OnSlotChanged(_outputSlot);

                UpdateLoading(_miningDrill.Percentage);
            }
            else if (_miningDrill != null)
            {
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

        private void UpdateLoading(float percentage)
        {
            _loading.fillAmount = percentage;
        }

    }
}
