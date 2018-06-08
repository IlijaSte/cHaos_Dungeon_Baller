using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BallRespawn : MonoBehaviour {

	public Transform t;
	public Rigidbody rb;
	public float respawnX;
	public float respawnY;
	public float respawnZ;
	public string nextLevel;
	private CollectManager cm;
	static public string staticNext;
	public PlaySimulation ps;
	public CanvasGroup panel;
	private static float lastPos;
	private int levelNum = 0;

	public Material origMaterial;

	void Start () {
		t = GetComponent<Transform>();
		rb = GetComponent<Rigidbody> ();
		staticNext = nextLevel;
		lastPos = respawnY;
		print (Application.persistentDataPath);

		cm = GameObject.Find ("UIManager").GetComponent<CollectManager> ();

		LevelNameHolder lnh = GameObject.Find ("GameController").GetComponent<LevelNameHolder> ();
		string levelName = "";
		foreach (char c in lnh.levelName) {
			if (c >= '0' && c <= '9') {
				levelName += c;
			}
		}

		levelNum = int.Parse (lnh.levelName);
		SaveManager.loadGame ();
	}

	static public void respawnBall(){
		GameObject ball = GameObject.Find ("Ball");
		BallRespawn br = ball.GetComponent<BallRespawn> ();
		ball.GetComponent<Transform>().position = new Vector3 (br.respawnX, br.respawnY, br.respawnZ);
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		CollectManager cm = GameObject.Find ("UIManager").GetComponent<CollectManager> ();

		GameObject collectibles = GameObject.Find ("Collectibles");
		foreach (Transform child in collectibles.transform) {

			child.gameObject.GetComponent<BoxCollider> ().enabled = true;
			child.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			//collectibles.transform.GetChild(index).GetComponent<BoxCollider> ().enabled = true;
			//collectibles.transform.GetChild(index).GetComponent<MeshRenderer> ().enabled = true;

		}
		cm.tempCollected.Clear ();
		GameObject.Find ("Placing").GetComponent<ElementPlacing> ().canPlace = true;


	}

	public int search(List<int> l, int elem){

		for (int i = 0; i < l.Count; i++) {
			if (l[i] == elem) {
				return i;
			}
		}

		return -1;

	}

	public void levelPassed(){

		lastPos = t.position.y;
		//PlaySimulation.isSimActive = false;
		GameObject stars = GameObject.Find("Stars");
		//Image[] starImages = stars.GetComponentsInChildren<Image>();
		GameObject.Find("Placing").GetComponent<ElementPlacing>().canPlace = true;
		foreach (int collectible in cm.tempCollected) {
			if (GameObject.Find ("Collectibles").transform.GetChild (collectible).GetComponent<MeshRenderer> ().material != cm.transpMat) {
				
				GameObject.Find ("Collectibles").transform.GetChild (collectible).GetComponent<MeshRenderer> ().material = cm.transpMat;
				GameObject.Find ("Collectibles").transform.GetChild (collectible).GetComponent<CollectDetector> ().collected = true;
				if (search(CollectManager.allCollected [int.Parse (GameObject.Find ("GameController").GetComponent<LevelNameHolder> ().levelName)], collectible) < 0) {
					CollectManager.allCollected [int.Parse (GameObject.Find ("GameController").GetComponent<LevelNameHolder> ().levelName)].Add (collectible); 
					cm.numCollected++;
					CollectManager.totalNumCollected++;
					print ("coll: " + collectible);
				}
			}
		}
		cm.tempCollected.Clear ();

		CollectManager.levelsPassed [levelNum + 1] = true;

		int count = stars.transform.childCount;
		int i = 0;

		foreach (Transform star in stars.transform) {
			if (GameObject.Find("Collectibles").transform.GetChild(i).GetComponent<CollectDetector>().collected) {
				star.gameObject.GetComponent<Image> ().sprite = star.parent.gameObject.GetComponent<StarScript>().gotImage;
				i++;
			} else{
				star.gameObject.GetComponent<Image> ().sprite = star.parent.gameObject.GetComponent<StarScript>().notGotImage;
				i++;
			}
		}


		panel.alpha = 1;
		panel.blocksRaycasts = true;
		SaveManager.saveGame ();
		return;

	}
}
