using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSelSwipe : MonoBehaviour {

	private Vector3 touchPos;
	private float initPos;
	public int towerHeight;

	private float lastY = 1;

    public Material solidMat;
    public Material transpMat;

    public Transform buttons;

    private Material[][] origMats;

    //public int firstButton = 0;

    [SerializeField]
    public GameObject[] transpWalls;

    public void Start()
    {
        origMats = new Material[towerHeight][];

        int i = 0;
        foreach(GameObject wall in transpWalls)
        {
            MeshRenderer mr = wall.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                origMats[i] = new Material[mr.materials.Length];
                //mr.materials.CopyTo(origMats[i], 0);
                int j = 0;
                foreach(Material mat in mr.materials)
                {
                    origMats[i][j] = new Material(mat);
                    j++;
                }

                //Array.Copy(mr.materials, origMats[i], mr.materials.Length);
            }
            i++;
        }

    }

    public void towerScroll(Vector2 vector){

		transform.position = new Vector3 (transform.position.x, transform.position.y - (lastY - vector.y) * 400, transform.position.z);
		lastY = vector.y;

        float inverted = 1 - vector.y;

        float minDiff = float.MaxValue;
        int currButton = -1;

        foreach(Transform btn in buttons)
        {
            if (Mathf.Abs(inverted - btn.GetSiblingIndex() / (float)(towerHeight - 1)) < minDiff)
            {
                minDiff = Mathf.Abs(inverted - btn.GetSiblingIndex() / (float)(towerHeight - 1));
                currButton = btn.GetSiblingIndex();

            }
        }

        int secondButton = -1;
        float secondDiff = float.MaxValue;

        foreach (Transform btn in buttons)
        {
            if (Mathf.Abs(inverted - btn.GetSiblingIndex() / (float)(towerHeight - 1)) < secondDiff && btn.GetSiblingIndex() != currButton)
            {
                secondDiff = Mathf.Abs(inverted - btn.GetSiblingIndex() / (float)(towerHeight - 1));
                secondButton = btn.GetSiblingIndex();

            }
        }

        print("closest: " + currButton);
        print("second: " + secondButton);

        float diff = 1 / (float)(towerHeight - 1);

        if(transpWalls[currButton].GetComponent<MeshRenderer>() != null)
        {

            int i = 0;

            MeshRenderer mr = transpWalls[currButton].GetComponent<MeshRenderer>();

            Material[] newMats = new Material[mr.materials.Length];
            foreach (Material mat in transpWalls[currButton].GetComponent<MeshRenderer>().materials)
            {
                newMats[i] = mr.materials[i];
                newMats[i].Lerp(transpMat, origMats[currButton][i], minDiff / diff);

                i++;
            }
            mr.materials = newMats;

        }
        if (transpWalls[secondButton].GetComponent<MeshRenderer>() != null)
        {

            int i = 0;

            MeshRenderer mr = transpWalls[secondButton].GetComponent<MeshRenderer>();

            Material[] newMats = new Material[mr.materials.Length];
            foreach (Material mat in transpWalls[secondButton].GetComponent<MeshRenderer>().materials)
            {
                newMats[i] = mr.materials[i];
                newMats[i].Lerp(transpMat, origMats[secondButton][i], secondDiff / diff);

                i++;
            }
            mr.materials = newMats;
        }

        /*float diff = Mathf.Abs(buttons.GetChild(0).position.y - buttons.GetChild(1).position.y);

        transpWalls[secondButton].GetComponent<MeshRenderer>().material.Lerp(transpMat, solidMat, secondDiff / diff);
        transpWalls[secondButton].GetComponent<MeshRenderer>().material.Lerp(solidMat, transpMat, minDiff / diff);*/

    }
}
