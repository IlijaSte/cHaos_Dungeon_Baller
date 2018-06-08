using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour {

	public void startLevel(string name){

		switch (name) {
		case "Floor2Selection":
			// samo ako ima vise od 1 zvezdice
			if (CollectManager.totalNumCollected >= 1)
				SceneManager.LoadScene (name);
			break;
		case "Floor3Selection":
			// samo ako ima vise od 2 zvezdice
			if (CollectManager.totalNumCollected >= 4)
				SceneManager.LoadScene (name);
			break;
		default:
			SceneManager.LoadScene (name);
			break;
		}

			

	}
}
