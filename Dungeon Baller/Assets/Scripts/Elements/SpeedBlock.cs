using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBlock : MonoBehaviour {

	public char dir;
	private int xdir;
	private int zdir;
	private GameObject ball;


	// Use this for initialization
	void Start () {
		xdir = zdir = 0;
		ball = GameObject.Find ("Ball");
		/*switch(dir){
		case 'd':

			xdir = 1;
			break;
		case 'u':

			xdir = -1;
			break;

		case 'r':

			zdir = 1;
			break;
		case 'l':

			zdir = -1;
			break;
		}*/

	}
	
	// Update is called once per frame
	void Update () {

		float rot = GetComponent<Transform> ().rotation.eulerAngles.y % 360;
		xdir = 0;
		zdir = 0;
		if (rot == 0) {
			dir = 'u';
			zdir = -1;
		}
		else if (rot == 90 || rot == -270) {
			dir = 'r';
			xdir = -1;

		}
		else if (Mathf.Abs (rot) == 180) {
			dir = 'd';
			zdir = 1;

		}
		else {
			dir = 'l';
			xdir = 1;
		}

		if ((Mathf.Abs(ball.transform.position.x - transform.position.x) < 0.75f) &&
			(Mathf.Abs(ball.transform.position.z - transform.position.z) < 0.75f) && (Mathf.Abs(ball.transform.position.y - transform.position.y) < 0.75f)) {

			ball.GetComponent<Rigidbody> ().AddForce(xdir * 8, 0, zdir * 8);
			//ball.GetComponent<Rigidbody> ().

		}

	}
}
