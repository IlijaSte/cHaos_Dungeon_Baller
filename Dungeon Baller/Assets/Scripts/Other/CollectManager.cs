using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollectManager : MonoBehaviour {

	public static int totalNumCollected = 0;
	public int numCollected = 0;

	public List<int> tempCollected = new List<int>();
	public List<int> collected = new List<int>();
	public static List<int>[] allCollected = new List<int>[256];
	public static bool[] levelsPassed = new bool[256];

	public Material transpMat;

	// Use this for initialization
	void Start () {

		SaveManager.loadGame ();
		int i = 0;
		LevelNameHolder nameHolder = GameObject.Find ("GameController").GetComponent<LevelNameHolder> ();
		GameObject collectibles = GameObject.Find ("Collectibles");
		if (allCollected [int.Parse (nameHolder.levelName)] != null) {
			foreach (int index in allCollected[int.Parse(nameHolder.levelName)]) {

				collectibles.transform.GetChild (index).GetComponent<MeshRenderer> ().material = transpMat;
				collectibles.transform.GetChild (index).GetComponent<CollectDetector> ().collected = true;
				i++;
			}
		}
	}

}
