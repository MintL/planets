using System;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class Provider : MonoBehaviour
    {
        public Slot OutputSlot { get; set; }
        public List<CargoController> Cargos { get; set; }

        public void PickUp(CargoController cargo, int count)
        {
            if (OutputSlot.Count > 0)
            {
                var maxCount = Math.Min(count, OutputSlot.Count);
                OutputSlot.Count -= maxCount;

                cargo.Slot.Item = OutputSlot.Item;
                cargo.Slot.Count = maxCount;

                cargo.Return();
            }
        }
    }
}
