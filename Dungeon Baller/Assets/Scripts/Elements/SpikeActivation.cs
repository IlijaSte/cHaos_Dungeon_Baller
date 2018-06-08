using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeActivation : MonoBehaviour {

	private Transform field;
	private GameObject ball;

	private bool movingIn;
	private bool movingOut;
	public float speed = 2f;
	private float i = 0.0f;
	private Rigidbody rb;

	private Vector3 initPos;
	private Vector3 endPos;
	private Transform t;
	private GameObject spikes;

	// Use this for initialization
	void Start () {
		field = transform.Find ("ActivateField");
		ball = GameObject.Find ("Ball");
		spikes = transform.Find ("Obj_000001").gameObject;
		rb = spikes.GetComponent<Rigidbody> ();
		t = spikes.GetComponent<Transform> ();

		initPos = t.position;
		endPos = t.position + new Vector3(t.forward.x * 0.25f, t.forward.y * 0.25f, t.forward.z * 0.25f);

	}



	// Update is called once per frame
	void FixedUpdate () {

		//if(spikes.GetComponent<MeshCollider>().

		if (field.GetComponent<BoxCollider> ().bounds.Contains (ball.transform.position)) {

			if (!movingOut && !movingIn) {
				movingOut = true;
				//movingIn = false;
			}

		}

		if (movingOut) {

			if (i < 1.0f) {

				i += Time.deltaTime * speed * 100;
				rb.MovePosition (Vector3.Lerp (initPos, endPos, i));
			} else {
				movingOut = false;
				movingIn = true;
				i = 0.0f;
			}

		} else if (movingIn) {

			if (i < 1.0f) {
				i += Time.deltaTime * (speed / 40f);

				rb.MovePosition (Vector3.Lerp (endPos, initPos, i));

			} else {
				movingIn = false;
			}
		}


	}
}
