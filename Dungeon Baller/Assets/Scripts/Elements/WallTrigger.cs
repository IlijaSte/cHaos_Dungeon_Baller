using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WallTrigger : MonoBehaviour {

	private Vector3 initPos;
	private Vector3 endPos;
	private Rigidbody rb;
	private Transform t;
	private bool moving = false;
	private bool movingIn = false;
	private bool movingOut = false;
	private float i = 0.0f;
	public float speed;
	private GameObject ball;
	public GameObject detector;


	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		t = GetComponent<Transform> ();
		ball = GameObject.Find ("Ball");
		initPos = t.position;

	}

	public void OnMouseDown(){



		if ((!moving) && PlaySimulation.isSimActive) {
			moving = true;
			movingOut = true;
			i = 0f;
			initPos = t.position;
			endPos = initPos + t.forward;
		}

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!PlaySimulation.isSimActive) {

			t.position = initPos;
			moving = movingOut = movingIn = false;
			i = 0;

		}

		if (PlaySimulation.isSimActive && (!moving) && detector && detector.GetComponent<BoxCollider> ().bounds.Contains (ball.transform.position)) {

			moving = true;
			movingOut = true;
			i = 0f;
			initPos = t.position;
			endPos = initPos + t.forward;

			ball.transform.rotation = Quaternion.Euler(Vector3.Scale(new Vector3(Mathf.Abs(t.right.x), Mathf.Abs(t.right.y), Mathf.Abs(t.right.z)), ball.transform.rotation.eulerAngles));
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.Scale (ball.GetComponent<Rigidbody>().angularVelocity, new Vector3 (Mathf.Abs (t.right.x), 0, Mathf.Abs (t.right.z)));
			ball.GetComponent<Rigidbody>().velocity = Vector3.Scale (ball.GetComponent<Rigidbody>().velocity, new Vector3 (Mathf.Abs (t.forward.x), 0, Mathf.Abs (t.forward.z)));

		}

		if (moving) {

			if (movingOut) {
				if (i < 1.0f) {
					i += Time.deltaTime * speed;
					rb.MovePosition (Vector3.Lerp (initPos, endPos, i));

				} else {
					movingOut = false;
					movingIn = true;
					i = 0f;
				}
			} else if (movingIn) {
				if (i < 1.0f) {
					i += Time.deltaTime * (speed / 10f);

					rb.MovePosition (Vector3.Lerp (endPos, initPos, i));

				} else {
					movingIn = false;
					//moving = false;
				}
			}
			moving = movingIn || movingOut;
		}

	}
}
