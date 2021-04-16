using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Planets
{
	public class PlayerController : MonoBehaviour
	{
		public float MovementBaseSpeed = 1.0f;

		public Vector2 movementDirection;
		public Rigidbody2D rigidBody;
		public Camera camera;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void FixedUpdate()
		{
			Move();
		}

		void OnMove(InputValue input)
		{
			Vector2 inputVec = input.Get<Vector2>();

			movementDirection = new Vector2(inputVec.x, inputVec.y);
			movementDirection.Normalize();
		}

		void Move()
		{
			rigidBody.velocity = movementDirection * MovementBaseSpeed;
		}

	}
}
