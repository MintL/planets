using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Planets
{
    public class SmoothCamera2D : MonoBehaviour
    {
        public float dampTime = 0.15f;
        private Vector3 _velocity = Vector3.zero;
        public Transform target;
        private Camera _camera;

        public void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (target)
            {
                var point = _camera.WorldToViewportPoint(target.position);
                var delta = target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                if (delta.magnitude < 0.01)
                {
                    return;
                }

                var destination = transform.position + delta;
                destination.z = -10;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
            }
        }
    }
}
