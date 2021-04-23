using UnityEngine;

namespace Planets
{
    public class Requester : MonoBehaviour
    {
        public Provider Provider;
        public GameObject CargoPrefab;

        private CargoController _cargo;
        private bool _cargoInbound;

        // Update is called once per frame
        void Update()
        {
            if (!_cargoInbound)
            {
                SendCargo(Provider);
            }
        }

        public void SendCargo(Provider provider)
        {
            Debug.Log("Send cargo");
            _cargoInbound = true;
            var child = Instantiate(CargoPrefab, transform.position, Quaternion.identity);
            _cargo = child.GetComponent<CargoController>();
            _cargo.SetupCargo(provider, this);
        }

        public void Ready()
        {
            // _inputSlot.Item = _cargo.Slot.Item;
            // _inputSlot.Count += _cargo.Slot.Count;
            //Debug.Log(_cargo.Slot.Count);

            _cargoInbound = false;
            SendMessage("OnCargo", _cargo.Slot);
        }
    }
}
