using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public abstract class Factory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public EventHandler<float> PercentageUpdated;
        public EventHandler<Slot> InputUpdated;
        public EventHandler<Slot> OutputUpdated;

        public int Percentage { get; }
        public Recipe Recipe { get; set; }

        public Slot InputSlot { get; set; }
        public Slot OutputSlot { get; set; }

        private bool _isActive = false;
        private float _timeSinceLast = 0f;

        // Start is called before the first frame update
        void Start()
        {
            Recipe = GetRecipe();
            InputSlot = new Slot
            {
                Item = Recipe.Input.Item,
            };
            OutputSlot = new Slot
            {
                Item = Recipe.Output.Item,
            };
            Debug.Log("Furnace ready");

            var requester = GetComponent<Requester>();
            if (requester != null)
            {
                requester.Slot = InputSlot;
            }

            var provider = GetComponent<Provider>();
            if (provider != null)
            {
                provider.OutputSlot = OutputSlot;
            }
        }

        protected abstract Recipe GetRecipe();

        public void Update()
        {
            if (!_isActive && InputSlot.Count >= Recipe.Input.Count)
            {
                InputSlot.Take(Recipe.Input.Count);
                _isActive = true;
            }

            if (_isActive)
            {
                _timeSinceLast += Time.deltaTime;

                PercentageUpdated?.Invoke(this, _timeSinceLast / Recipe.CraftSpeed);
                if (_timeSinceLast > Recipe.CraftSpeed)
                {
                    OnTime();
                    _timeSinceLast = 0;
                    _isActive = false;
                }
            }
        }

        private void OnTime()
        {
            OutputSlot.Add(Recipe.Output.Count);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                FindObjectOfType<UIController>().Show(Panels.Recipe, this);
            }
        }
    }
}
