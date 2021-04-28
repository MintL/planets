using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Planets
{
    public class RecipeController : MonoBehaviour, IPanel
    {
        private Image _loading;
        private Text _speedText;
        private Text _inputText;
        private Text _outputText;

        private Slot _inputSlot;
        private Slot _outputSlot;
        private Factory _factory;
        private SlotController _inputController;
        private SlotController _outputController;

        private void Start()
        {
            _loading = transform.Find("Output").Find("Loading").Find("Fill").GetComponent<Image>();
            _loading.fillAmount = 0;

            _speedText = transform.Find("Output").Find("Speed").GetComponent<Text>();

            _inputText = transform.Find("Input").Find("Text").GetComponent<Text>();
            _outputText = transform.Find("Output").Find("Text").GetComponent<Text>();

            _inputController = transform.Find("Input").Find("Slot").GetComponent<SlotController>();
            _outputController = transform.Find("Output").Find("Slot").GetComponent<SlotController>();
            // _slotController.Clicked += OnOutputClicked;
        }

        public void OnShow()
        {

        }

        public void OnElementChanged(object element)
        {
            if (element is Factory factory)
            {
                _factory = factory;
                _factory.PercentageUpdated += OnPercentageUpdated;
                _outputSlot = _factory.OutputSlot;
                _outputController.OnSlotChanged(_outputSlot);

                _inputSlot = _factory.InputSlot;
                _inputController.OnSlotChanged(_inputSlot);

                UpdateRecipe(_factory.Recipe);

                UpdateLoading(factory.Percentage);
            }
            else if (_factory != null)
            {
                _factory = null;
            }
        }

        // private void OnOutputClicked(object sender, PointerEventData e)
        // {
        //     if (sender is Slot slot)
        //     {
        //         _miningDrill.PickUp(slot);
        //     }
        // }

        private void OnPercentageUpdated(object sender, float percentage)
        {
            UpdateLoading(percentage);
        }

        private void UpdateLoading(float percentage)
        {
            _loading.fillAmount = percentage;
        }

        private void UpdateRecipe(Recipe recipe)
        {
            _speedText.text = recipe.CraftSpeed + " sec";
            _inputText.text = recipe.Input.Count + " " + recipe.Input.Item.Name;
            _outputText.text = recipe.Output.Count + " " + recipe.Output.Item.Name;
        }
    }
}
