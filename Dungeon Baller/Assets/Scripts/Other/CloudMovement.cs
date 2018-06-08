using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	public Vector3 speed = Vector3.zero;
	void Update () {
		float time = Time.deltaTime;
		// pomeraj za vektor brzine
		transform.Translate (time * speed.x, time * speed.y, time * speed.z);

		// unistavanje ako je izasao van opsega
		if (transform.position.x < -5000 || transform.position.z > 8000) {
			Destroy (gameObject);
		}

	}
}
