using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedUp : MonoBehaviour {

	private static bool goingOver = false;
	public bool goingOverThis = false;
	public bool goingUp = false;
	Rigidbody brb;
	GameObject ball;
	Transform brother = null;
	private Vector3 startPos;
	private static Vector3 startVel;

	private Vector3 holdVelocity;
	void Awake(){
		ball = GameObject.Find ("Ball");
		brb = ball.GetComponent<Rigidbody> ();
		if (name == transform.parent.GetChild (0).name) {
			brother = transform.parent.GetChild (1);
		} else
			brother = transform.parent.GetChild (0);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Ball") {

			if(!goingOver){



				float maxY = -35565;
				Transform maxChild = transform;
				foreach (Transform child in transform.parent) {
					if (child.position.y > maxY) {
						maxChild = child;
						maxY = child.position.y;
					}
				}

				if (transform == maxChild) {
					goingUp = false;
				} else {
					goingUp = true;

					goingOver = true;
					goingOverThis = true;
				}
				print (goingUp);

			}

		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.name == "Ball") {

			if (goingOver && !goingOverThis) {

				goingOver = false;

			}

		}
	}

	void FixedUpdate(){

		if (!PlaySimulation.isSimActive) {
			goingOverThis = false;
			goingOver = false;
			goingUp = false;
		}
		if (!goingOver) {
			goingOverThis = false;
			goingUp = false;
		}
		
		if (goingOverThis && goingUp) {
			float intensity = brb.mass * 9.81f * 0.447f + 0.5f;
			if((Mathf.Abs(brb.velocity.x) > Mathf.Abs(brb.velocity.z)) && Mathf.Abs(brb.velocity.x) <= 5){
				brb.AddForce (Mathf.Sign(brb.velocity.x) * intensity, (intensity - 0.5f) / 3f, 0, ForceMode.Force);
			}else if((Mathf.Abs(brb.velocity.z) > Mathf.Abs(brb.velocity.x)) && Mathf.Abs(brb.velocity.z) <= 5) {
				brb.AddForce (0, (intensity - 0.5f) / 3f, Mathf.Sign(brb.velocity.z) * intensity, ForceMode.Force);
			}

		}


	}
}
