using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpike : MonoBehaviour {
	//public GameObject ball;
	public BallRespawn br;
	public PlaySimulation ps;

	public void Start(){

		ps = GameObject.Find ("UIManager").GetComponent<PlaySimulation> ();

	}
	//void OnCollisionEnter(Collision col){
	IEnumerator OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Ball") {



			yield return new WaitForSeconds(0.5f);
			if(PlaySimulation.isSimActive)
				ps.PlaySim ();
			//br.respawnBall ();

		}

	}
}
