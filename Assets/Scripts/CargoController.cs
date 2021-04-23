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

        public Slot Slot { get; set; }

        private void Start()
        {
            Slot = new Slot();
        }

        public void SetupCargo(Provider provider, Requester requester)
        {
            _provider = provider;
            _requester = requester;
            _targetPos = provider.transform.position;
            _direction = (_targetPos - transform.position).normalized;

            _isActive = true;
        }

        public void Return()
        {
            _targetPos = _requester.transform.position;
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

                if (dist < 0.01)
                {
                    _isActive = false;

                    if (Slot.Count == 0)
                    {
                        _provider.PickUp(this, MaxCapacity);
                    }
                    else
                    {
                        _requester.Ready();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
