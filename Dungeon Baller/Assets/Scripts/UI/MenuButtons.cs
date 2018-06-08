using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public void StartGame(string sceneName){

		SceneManager.LoadScene (sceneName);
		SaveManager.loadGame ();

	}

	public void exitGame(){
		Application.Quit ();
	}
}
