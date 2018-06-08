using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotate : MonoBehaviour {

	Touch initTouch = new Touch();
	// Use this for initialization
	public float rotSpeed = 0.3f;
	public float dir = 1;
	private Transform t;
	private float i = 0f;
	private Vector3 touchPos;
	private Quaternion touchRot;
	private Quaternion minRot;
	private Quaternion maxRot;
	private Vector3 minPos;
	private Vector3 maxPos;
	public TopView tv;

	public GameObject turnInvisible;

	private bool movingUp = false;
	private bool movingDown = false;

	private bool isTop = false;
	void Start () {
		t = GetComponent<Transform> ();
		minRot = t.rotation;
		minPos = t.position;
		maxPos = new Vector3 (-0.5f, 7, -0.5f);
		maxRot = Quaternion.Euler(new Vector3 (90f, tv.topRotY, 0));
		rotSpeed = 1.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!movingUp && !movingDown) {

			foreach (Touch touch in Input.touches) {



				if (touch.phase == TouchPhase.Began) {
					initTouch = touch;
					touchPos = t.position;
					touchRot = t.rotation;

				} else if (touch.phase == TouchPhase.Moved) {

					float deltaY = initTouch.position.y - touch.position.y;

					if (deltaY > 0 && !isTop) {


						i += rotSpeed * Time.deltaTime;
						if (i > 0.25f) {

							movingUp = true;
							turnInvisible.GetComponent<MeshRenderer> ().enabled = false;
							i = 0f;
							initTouch = new Touch ();
						}

					} else if (deltaY < 0 && isTop) {

						i -= rotSpeed * Time.deltaTime;
						if (i < -0.25f) {

							movingDown = true;
							tv.transpWall.GetComponent<MeshRenderer> ().material = tv.transpMat;
							turnInvisible.GetComponent<MeshRenderer> ().enabled = true;
							i = 0f;
							initTouch = new Touch ();
						}

					}

					initTouch = touch;

				} else if (touch.phase == TouchPhase.Ended) {
					i = 0f;

					initTouch = new Touch ();
				}

			}
			

		} else if (movingUp) {

			if (i < 1.0f) {
				i += Time.deltaTime * rotSpeed;

				t.rotation = Quaternion.Euler (Vector3.Lerp (minRot.eulerAngles, maxRot.eulerAngles, i));
				t.position = Vector3.Lerp (minPos, maxPos, i);
				//rb.MovePosition (Vector3.Lerp (oldCamPos, newPos, i));
				//rb.MoveRotation (Quaternion.Euler (Vector3.Lerp (oldCamRot.eulerAngles, newRot, i)));
			} else {
				movingUp = false;
				isTop = true;
				tv.transpWall.GetComponent<MeshRenderer> ().material = tv.normMat;
				i = 0f;
			}

		} else {

			if (i < 1.0f) {
				i += Time.deltaTime * rotSpeed;

				t.rotation = Quaternion.Euler (Vector3.Lerp (maxRot.eulerAngles, minRot.eulerAngles, i));
				t.position = Vector3.Lerp (maxPos, minPos, i);

				//rb.MovePosition (Vector3.Lerp (newPos, oldCamPos, i));
				//rb.MoveRotation (Quaternion.Euler (Vector3.Lerp (newRot, oldCamRot.eulerAngles, i)));

			} else {
				movingDown = false;
				isTop = false;
				i = 0f;
				//moving = false;
			}

		}

	}
}
