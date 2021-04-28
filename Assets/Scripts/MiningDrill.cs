using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Planets
{
    public class MiningDrill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public EventHandler<float> PercentageUpdated;
        public EventHandler<Slot> SlotUpdated;

        public float Percentage { get; set; }
        public Slot Slot;

        private InventorySystem _inventory;
        private ItemDatabase _itemDatabase;

        private float _miningSpeed = 0.25f;
        private float _timeSinceLast = 0f;
        private bool _isActive = true;

        public void Start()
        {
            _inventory = FindObjectOfType<InventorySystem>();
            _itemDatabase = FindObjectOfType<ItemDatabase>();

            Slot = new Slot
            {
                Item = _itemDatabase.GetItem("Iron Ore"),
                Count = 0,//(int)(UnityEngine.Random.value * 100f),
            };

            var provider = GetComponent<Provider>();
            provider.OutputSlot = Slot;
        }

        public void PickUp(Slot slot)
        {
            //_inventory.PickUp(slot);
            Slot.Count = 0;
        }

        public void Update()
        {
            if (_isActive)
            {
                _timeSinceLast += Time.deltaTime;

                PercentageUpdated?.Invoke(this, _timeSinceLast / (1f / _miningSpeed));
                if (_timeSinceLast > 1f / _miningSpeed)
                {
                   OnTime();
                   _timeSinceLast = 0;
                }
            }
        }

        private void OnTime()
        {
            Slot.Add(1);
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
                FindObjectOfType<UIController>().Show(Panels.MiningDrill, this);
            }
        }
    }
}
