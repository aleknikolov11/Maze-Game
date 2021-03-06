using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform lookAt;
	private Vector3 offset;
	private Vector3 desiredPosition;
	private float distance = 0.5f;
	private float yOffset = 1.5f;
	private float smoothSpeed = 5f;


	private void Start () {
		offset = new Vector3  (-1.0f * distance, yOffset, 0);
	}

	private void FixedUpdate() {
		
		desiredPosition = lookAt.position + offset;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.LookAt (lookAt.position );


	}
}
