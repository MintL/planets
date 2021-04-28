using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Planets
{
    public class Requester : MonoBehaviour
    {
        public Slot Slot;

        private CargoController _cargo;
        private bool _cargoInbound;

        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Drop(CargoController cargo)
        {
            Slot.Add(cargo.Slot.Take());
            // SendMessage("OnCargo");
        }
    }
}
