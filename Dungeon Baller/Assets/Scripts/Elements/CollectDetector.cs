using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectDetector : MonoBehaviour {

	CollectManager cm;

	public GameObject ball;
	private Transform transf;
	private int dir = -1;
	private float yoff = 0;

	public bool collected = false;

	void Start(){
		cm = GameObject.Find ("UIManager").GetComponent <CollectManager>();
		transf = GetComponent<Transform> ();

	}
	void Update(){


		if (GetComponent<BoxCollider> ().bounds.Contains (ball.transform.position)) {

			if (GetComponent<MeshRenderer> ().material != cm.transpMat) {
				int i = 0;
				int index = 0;

				foreach (Transform child in transform.parent.transform) {
					if (child == transform) {
						index = i;
						break;
					}
					i++;
				}
				cm.tempCollected.Add (index);
				//GetComponent<BoxCollider> ().enabled = false;
			}
			GetComponent<MeshRenderer> ().enabled = false;

		} else {

			transf.Rotate (new Vector3 (0, -100 * Time.deltaTime, 0));
			float temp = dir * 0.1f * Time.deltaTime;
			yoff += Mathf.Abs (temp);
			transf.Translate (new Vector3 (0, temp, 0));
			if (yoff > 0.15f) {
				dir = -dir;
				yoff = 0;
			}
		}
	}
}
