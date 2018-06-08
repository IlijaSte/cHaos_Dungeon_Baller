using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {

	public LevelSwipeSel lss;


	public void selectLevel(){

		SceneManager.LoadScene (lss.camPositions.transform.GetChild (lss.curPos).gameObject.GetComponent<LevelNameHolder> ().levelName);

	}
}
