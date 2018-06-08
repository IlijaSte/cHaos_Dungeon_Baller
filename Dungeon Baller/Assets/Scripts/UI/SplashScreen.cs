using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour {

	public CanvasGroup panel;
	public PlaySimulation ps;

	public void restart(){
		BallRespawn.respawnBall ();
		ps.PlaySim ();
		panel.alpha = 0;
		panel.blocksRaycasts = false;
		GameObject.Find ("UIManager").GetComponent<CollectManager> ().tempCollected.Clear ();
		GameObject.Find ("Placing").GetComponent<ElementPlacing> ().canPlace = true;
	}

	public void nextLevel(){
		SceneManager.LoadScene (BallRespawn.staticNext);
	}
}
