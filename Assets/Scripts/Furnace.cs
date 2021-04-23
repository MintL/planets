using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class Furnace : MonoBehaviour
    {
        private Slot _inputSlot;
        private Slot _outputSlot;


        // Start is called before the first frame update
        void Start()
        {
            _inputSlot = new Slot();
            _outputSlot = new Slot();
            Debug.Log("Furnace ready");
        }

        public void OnCargo(Slot inboundSlot)
        {
            _inputSlot.Item = inboundSlot.Item;
            _inputSlot.Count += inboundSlot.Count;
        }
    }
}
