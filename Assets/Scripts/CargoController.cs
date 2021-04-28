using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class CargoController : MonoBehaviour
    {
        public float Speed = 1;
        public int MaxCapacity = 1;

        private Vector3 _targetPos;
        private Vector3 _direction;
        private bool _isActive;

        private Provider _provider;
        private Requester _requester;

        public Slot Slot { get; set; } = new Slot();

        public void SetupCargo(Provider provider, Requester requester, Slot slot)
        {
            _provider = provider;
            _requester = requester;
            _targetPos = requester.transform.position;
            _direction = (_targetPos - transform.position).normalized;

            Slot.Item = slot.Item;
            Slot.Count = slot.Take(MaxCapacity);
            Debug.Log(Slot.Count);

            _isActive = true;
        }

        public void Return()
        {
            _targetPos = _provider.transform.position;
            _direction = (_targetPos - transform.position).normalized;

            _isActive = true;
        }

        public void Update()
        {
            Debug.DrawLine(transform.position, _targetPos, Color.red);

            var dist = (_targetPos - transform.position).magnitude;
            if (_isActive)
            {
                var deltaPos = _direction * Time.deltaTime * Speed;
                transform.position += deltaPos;

                if (dist < 0.5)
                {
                    _isActive = false;

                    if (Slot.Count > 0)
                    {
                        _requester.Drop(this);
                        Return();
                    }
                    else
                    {
                        _provider.Ready(this);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
