using System;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class Provider : MonoBehaviour
    {
        public GameObject CargoPrefab;
        public int MaxCargo = 1;

        public Slot OutputSlot { get; set; }
        private List<CargoController> _cargos = new List<CargoController>();

        private IList<Requester> _requesters;

        private void Start()
        {
            _requesters = GameObject.FindObjectsOfType<Requester>();
        }

        private void Update()
        {
            if (_cargos.Count < MaxCargo && OutputSlot.Count > 0)
            {
                foreach (var requester in _requesters)
                {
                    if (requester.Slot.Item == OutputSlot.Item && requester.Slot.Count < 4)
                    {
                        SendCargo(requester);
                        break;
                    }
                }
            }
        }

        public void SendCargo(Requester requester)
        {
            Debug.Log("Send cargo");

            var child = Instantiate(CargoPrefab, transform.position, Quaternion.identity);
            var cargo = child.GetComponent<CargoController>();
            cargo.SetupCargo(this, requester, OutputSlot);
            _cargos.Add(cargo);
        }

        public void Ready(CargoController cargo)
        {
            _cargos.Remove(cargo);
        }

    }
}
