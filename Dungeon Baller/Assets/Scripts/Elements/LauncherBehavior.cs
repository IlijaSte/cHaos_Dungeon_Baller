using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBehavior : MonoBehaviour {

	GameObject ball;

	private bool movingOut = false;
	private bool movingIn = false;
	private float i = 0f;
	public float launchSpeed;
	private Rigidbody rb;
	private Transform t;
	private Vector3 initRot;
	private Vector3 endRot;
	private Vector3 pivot;

	private Vector3 initPos;
	private Vector3 finalVelocity;
	private Rigidbody ballRb;

	public int blocksToJump;

	Vector3 target;

	float initAngle = 45;

	void Start(){
		ball = GameObject.Find ("Ball");
		ballRb = ball.GetComponent<Rigidbody> ();
		rb = GetComponent<Rigidbody> ();
		t = GetComponent<Transform> ();
		initRot = transform.rotation.eulerAngles;
		initPos = transform.position;

		endRot = (t.localRotation.x != 0 ? new Vector3 (0, 0, 30) : new Vector3 (30, 0, 0));

		pivot = t.forward;
		pivot.Scale (new Vector3 (0.5f, 0.5f, 0.5f));

		target = ball.transform.position + blocksToJump * t.forward;
		float gravity = Physics.gravity.magnitude;
		float angle = initAngle * Mathf.Deg2Rad;

		Vector3 planarTarget = new Vector3 (target.x, 0, target.z);
		Vector3 planarPosition = new Vector3 (ball.transform.position.x, 0, ball.transform.position.z);

		float distance = Vector3.Distance (planarTarget, planarPosition);
		float yOffset = ball.transform.position.y - target.y;
	
		float initVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

		Vector3 velocity = new Vector3 (initVelocity * Mathf.Cos(angle) * t.forward.x, initVelocity * Mathf.Sin (angle), initVelocity * Mathf.Cos (angle) * t.forward.z);

		float angleBetween = Vector3.Angle (t.forward, planarTarget - planarPosition);
		finalVelocity = Quaternion.AngleAxis (angleBetween, Vector3.up) * velocity;


	}

	// Update is called once per frame
	void Update () {

		if (!PlaySimulation.isSimActive) {
			t.rotation = Quaternion.Euler (initRot);
			t.position = initPos;
			movingIn = false;
			movingOut = false;
		}
		if (Mathf.Abs(ball.transform.position.x - transform.position.x) < 0.2f && Mathf.Abs(ball.transform.position.y - transform.position.y) < 0.45f &&Mathf.Abs(ball.transform.position.z - transform.position.z) < 0.2f && !movingOut && !movingIn) {
			Vector3 scaled = Vector3.Scale (ballRb.velocity, transform.forward);
			if ((scaled != Vector3.zero) && (((Mathf.Abs(scaled.x) > Mathf.Abs(scaled.z)) && scaled.x > 0) || ((Mathf.Abs(scaled.z) > Mathf.Abs(scaled.x)) && scaled.z > 0))) {
				ball.transform.position = new Vector3 (transform.position.x, ball.transform.position.y, transform.position.z);

				movingOut = true;

				ballRb.velocity = Vector3.zero;
				ballRb.angularVelocity = Vector3.zero;
				ballRb.AddForce (finalVelocity * ballRb.mass, ForceMode.Impulse);
			}
		}



		if (movingOut) {

			float subX = Mathf.Abs(Mathf.Abs(endRot.x) % 360 - Mathf.Abs(t.rotation.eulerAngles.x) % 360);
			float subY = Mathf.Abs(Mathf.Abs(endRot.z) % 360 - Mathf.Abs(t.rotation.eulerAngles.z) % 360);

			if (subX > 2f || subY > 2f) {

				t.RotateAround (initPos + pivot, t.right, Time.deltaTime * launchSpeed);


			} else {
				//t.rotation = Quaternion.Euler(new Vector3(endRot.x, t.rotation.eulerAngles.y, endRot.z));
				//t.rotation.eulerAngles.z = endRot.z;
				movingIn = true;
				movingOut = false;
				i = 0f;
			}

		} else if (movingIn) {

			float subX = Mathf.Abs(Mathf.Abs(initRot.x) % 360 - Mathf.Abs(t.rotation.eulerAngles.x) % 360);
			float subY = Mathf.Abs(Mathf.Abs(initRot.z) % 360 - Mathf.Abs(t.rotation.eulerAngles.z) % 360);

			if (subX > 2f || subY > 2f) {

				t.RotateAround (initPos + pivot, t.right, -Time.deltaTime * launchSpeed / 10);


			} else {
				t.rotation = Quaternion.Euler(new Vector3(initRot.x, t.rotation.eulerAngles.y, initRot.z));
				t.position = initPos;
				movingIn = false;
				movingOut = false;
				i = 0f;
			}

		}

	}
}
