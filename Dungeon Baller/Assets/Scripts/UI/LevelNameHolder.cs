using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNameHolder : MonoBehaviour {

	public string levelName;
	public GameObject transpWall;
	public Material transpMaterial;
	public Material[] origMaterials;
	public bool lockedPreview;
	public bool notInCircle = false;

	void Awake(){
		if(transpWall)
			origMaterials = transpWall.GetComponent<MeshRenderer> ().materials;

	}
}
