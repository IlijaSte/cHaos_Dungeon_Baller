using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
public class PlaySimulation : MonoBehaviour {

	static public bool isSimActive = false;
	private Sprite oldSprite;
	private GameObject ball;
	public BallRespawn br;
	public WallTrigger wt;
	public Positioning positioning;
	void Start(){

		oldSprite = GameObject.Find ("PlaySimButton").GetComponent<Image> ().sprite;
		ball = GameObject.Find ("Ball");
		isSimActive = false;
		positioning = GameObject.Find ("UIManager").GetComponent<Positioning> ();
	}

	public void PlaySim(){

		Positioning.hideButtons ();
		Positioning.isPositioning = false;

		BlockHover.hideGrid ();
		BlockHover.hideRampGrid ();

		foreach (GameObject child in GameObject.FindGameObjectsWithTag("Spawned Objects")) {

			foreach (MonoBehaviour script in child.GetComponents<MonoBehaviour>()) {

				if (!script.name.Equals ("ElementPlacing")) {

					if (!isSimActive)
						script.enabled = true;
					else
						script.enabled = false;
				}

			}

		}

		if (!isSimActive) {
			GameObject.Find ("PlaySimButton").GetComponent<Image> ().sprite = GameObject.Find ("StopSprite").GetComponent<SpriteRenderer> ().sprite;
			//ball.transform.position = new Vector3 (2, 0.21f, -3.07f);


			BallRespawn.respawnBall ();
			ball.GetComponent<Rigidbody> ().isKinematic = false;
			ball.GetComponent<Rigidbody> ().detectCollisions = true;
		}
		else {
			GameObject.Find ("PlaySimButton").GetComponent<Image> ().sprite = oldSprite;
			BallRespawn.respawnBall ();
			//ball.transform.position = new Vector3 (2, 0.21f, -3.07f);
			ball.GetComponent<Rigidbody> ().isKinematic = true;
			ball.GetComponent<Rigidbody> ().detectCollisions = false;
			ball.GetComponent<Rigidbody> ().velocity = Vector2.zero;
			ball.GetComponent<Rigidbody> ().angularVelocity = Vector2.zero;
		}

		isSimActive = !isSimActive;
		if (isSimActive) {
			wt.OnMouseDown ();
		}

	}
}
