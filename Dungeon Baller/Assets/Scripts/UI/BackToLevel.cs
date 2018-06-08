using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLevel : MonoBehaviour {

	public void backToLevel(){
		SceneManager.LoadScene ("LevelSelection");
	}

}
