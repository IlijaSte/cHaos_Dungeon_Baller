using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour {

	void OnTriggerExit(Collider col){

		if (col.gameObject.name == "Ball") {
			col.gameObject.GetComponent<BallRespawn> ().levelPassed ();
		}
	}
}
