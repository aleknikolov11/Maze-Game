using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	private Rigidbody controller;
	private Transform camTransform;
	public JoystickController joystick;
	public bool gameOver;
	public bool collect;
	private float moveSpeed = 2f;
	private float drag  = 0.5f;
	private float terminalRotationSpeed = 0.5f;


	// Use this for initialization
	void Start () {
		controller = GetComponent<Rigidbody> ();
		controller.maxAngularVelocity = terminalRotationSpeed;
		controller.drag = drag;
		camTransform = Camera.main.transform;
		gameOver = false;
		collect = false;

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 dir = Vector3.zero;
		if (joystick.InputDirection != Vector3.zero && !gameOver) {
			dir = joystick.InputDirection;
		}

			Vector3 rotatedDir = camTransform.TransformDirection (dir);
			rotatedDir = new Vector3 (rotatedDir.x, 0f, rotatedDir.z);
			rotatedDir = rotatedDir.normalized * dir.magnitude;
			controller.velocity = Vector3.Lerp (controller.velocity, moveSpeed*rotatedDir, Time.deltaTime);

	}

	private void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "FinishEffect") {
			gameOver = true;
		}
		if (col.gameObject.tag == "Shard") {
			collect = true;
			Destroy (col.gameObject);
		}
	}
}