using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwipeSel : MonoBehaviour {

	public int curPos;
	private int nextPos = 1;
	private float i = 0f;
	private float j = 0f;
	private float k = 0f;
	private float deltaY;
	public GameObject camPositions;
	public float rotSpeed;
	private bool moving = false;
	public Camera camera;
	private Transform camtr;
	public Button selectButton;
	private int dir = 1;
	private float rotateAngle = 0f;
	public bool movedByOffset;
	private GameObject oldPos;
	private GameObject newPos;

	private GameObject dlight;
	private GameObject go = null;

	private float startLightIntensity;
	private float endLightIntensity;

	private bool darken = false;
	private bool brighten = false;

	LevelNameHolder lnhCur;
	LevelNameHolder lnhNext;

	void Start () {

		camtr = camera.GetComponent<Transform> ();
		i = 0f;
		k = 0f;
		moving = false;

		// postavljanje svih materijala zida na transparentne
		LevelNameHolder lnm = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
		if (lnm.transpWall) {
			int numOfMats = lnm.transpWall.GetComponent<MeshRenderer> ().materials.Length;
			Material[] mats = new Material[numOfMats];
			for (int j = 0; j < numOfMats; j++) {
				mats [j] = lnm.transpMaterial;
			}
			lnm.transpWall.GetComponent<MeshRenderer> ().materials = mats;
			lnm.transpWall.GetComponent<MeshRenderer> ().material = lnm.transpMaterial;
		}

		// postavljanje intenziteta svetla i tame
		dlight = GameObject.Find ("Directional Light");
		startLightIntensity = dlight.GetComponent<Light> ().intensity;
		endLightIntensity = dlight.GetComponent<Light> ().intensity - 0.6f;

		// ako nije predjen nivo, tama
		if (!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
			dlight.GetComponent<Light> ().intensity = endLightIntensity;
		}
	}
		
	private Vector2 initClickPos;

	// razlika dva ugla
	float sub2angles(float a, float b){

		float d = Mathf.Abs(a - b) % 360; 
		float r = d > 180 ? 360 - d : d;

		//calculate sign 
		int sign = (a - b >= 0 && a - b <= 180) || (a - b <=-180 && a- b>= -360) ? 1 : -1; 
		r *= sign;
		return r;
	}

	public static int extractNumbers(string s){

		string nums = "";
		foreach (char c in s) {
			if (c >= '0' && c <= '9') {
				nums += c;
			}
		}
		return int.Parse (nums);

	}

	bool changedMat = false;

	void FixedUpdate () {

		// klik
		if (Input.GetMouseButtonDown (0)) {

			initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			i = 0;
		// pusten klik
		} else if (Input.GetMouseButtonUp (0)) {

			i = 0;
		}

		// sprecavanje pokretanja rotiranja dok se krece kamera
		if (!moving) {

			// drzi se klik
			if (Input.GetMouseButton (0)) {

				float deltaX = initClickPos.x - Input.mousePosition.x;

				if (Mathf.Abs (deltaX) > Screen.width / 5) {
					return;
				}
				print (deltaX);
				if (deltaX > 0) {

					if (dir == 1) {
						i = 0f;
						dir = -1;

					}
					else

						i += deltaX;
					initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

					if (i > Screen.width / 5) {
						
						i = 0f;
						k = 0f;
						dir = -1;

						nextPos = curPos - 1;

						if (nextPos < 0) {
							nextPos = camPositions.transform.childCount - 1;
						}


						lnhCur = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
						lnhNext = camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ();
						print (lnhNext.lockedPreview);
						if (lnhNext.lockedPreview) {
							moving = true;
							if (!CollectManager.levelsPassed [extractNumbers (lnhNext.levelName)]) {
								if (CollectManager.levelsPassed [extractNumbers (lnhCur.levelName)])
									darken = true;
								j = 0f;
							} else {
								if (!CollectManager.levelsPassed [extractNumbers (lnhCur.levelName)])
									brighten = true;
								j = 0f;
							}

							oldPos = camPositions.transform.GetChild (curPos).gameObject;
							newPos = camPositions.transform.GetChild (nextPos).gameObject;
					

							selectButton.enabled = false;
							selectButton.GetComponent<Image> ().enabled = false;
							selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							if (go) {
								camtr.transform.parent = null;
								Destroy (go);
							}
							if (!lnhCur.notInCircle && !lnhNext.notInCircle) {
								go = new GameObject ();
								go.transform.position = Vector3.zero;
								go.transform.rotation = Quaternion.Euler (new Vector3 (0, camtr.rotation.eulerAngles.y, 0));
								transform.parent = go.transform;
							}
						}
			
					}

				} else if (deltaX < 0) {
					if (dir == -1) {
						i = 0;
						dir = 1;

					}
					else
						i += deltaX;
					initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					if (i < -Screen.width / 5) {


						i = 0f;
						k = 0f;

						dir = 1;

						nextPos = curPos + 1;

						if (nextPos == camPositions.transform.childCount)
							nextPos = 0;

						lnhCur = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
						lnhNext = camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ();

						if (lnhNext.lockedPreview) {

							moving = true;
							if (!CollectManager.levelsPassed [extractNumbers (lnhNext.levelName)]) {
								if (CollectManager.levelsPassed [extractNumbers (lnhCur.levelName)]) {
									darken = true;
									j = 0f;
								}
							} else {
							
								if (!CollectManager.levelsPassed [extractNumbers (lnhCur.levelName)]) {
									brighten = true;
									j = 0f;
								}
							}

							oldPos = camPositions.transform.GetChild (curPos).gameObject;
							newPos = camPositions.transform.GetChild (nextPos).gameObject;
					
							selectButton.enabled = false;
							selectButton.GetComponent<Image> ().enabled = false;
							selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							if (go) {
								camtr.transform.parent = null;
								camtr.rotation = Quaternion.Euler (camtr.rotation.eulerAngles.x, go.transform.rotation.eulerAngles.y, camtr.rotation.eulerAngles.z);
								Destroy (go);
							}
							if (!lnhCur.notInCircle && !lnhNext.notInCircle) {
								go = new GameObject ();
								go.transform.position = Vector3.zero;
								go.transform.rotation = Quaternion.Euler (new Vector3 (0, camtr.rotation.eulerAngles.y, 0));
								transform.parent = go.transform;
							}
						}

					}


				}

				//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

			}
				
		}else {

			float tmp = Time.deltaTime;
			k += (tmp * rotSpeed);

			float a = camtr.rotation.eulerAngles.y;
			float b = newPos.transform.rotation.eulerAngles.y;



			float r = sub2angles (a, b);

			float p = sub2angles (oldPos.transform.rotation.eulerAngles.y, newPos.transform.rotation.eulerAngles.y);

			float offset = movedByOffset ? -0.5f : 0;

			Vector3 rotA = new Vector3(0, camtr.rotation.eulerAngles.y, 0);
			Vector3 rotB = new Vector3(0, newPos.transform.rotation.eulerAngles.y, 0);

			int numChild = 0;
			foreach (LevelNameHolder lnh in camPositions.GetComponentsInChildren<LevelNameHolder>()) {
				if (lnh.lockedPreview)
					numChild++;
			}
			if(k < 1){
				LevelNameHolder lnmOld = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
				LevelNameHolder lnmNew = camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ();
				if (sub2angles (camPositions.transform.GetChild (curPos).rotation.eulerAngles.y, camPositions.transform.GetChild (nextPos).rotation.eulerAngles.y) == 180) {
					go.transform.rotation = Quaternion.Euler (0, Mathf.Lerp (oldPos.transform.rotation.eulerAngles.y, oldPos.transform.rotation.eulerAngles.y + dir * 180, k), 0);

				} else {
					if (lnmOld.notInCircle || lnmNew.notInCircle) {
						camtr.position = Vector3.Lerp (oldPos.transform.position, newPos.transform.position, k);
						camtr.rotation = Quaternion.Euler (camtr.rotation.eulerAngles.x, Quaternion.Slerp (oldPos.transform.rotation, newPos.transform.rotation, k).eulerAngles.y, camtr.rotation.eulerAngles.z);

					}else
						go.transform.rotation = Quaternion.Euler (0, Quaternion.Slerp (oldPos.transform.rotation, newPos.transform.rotation, k).eulerAngles.y, 0);

				}

				if ((lnmOld.transpWall != null) && (Mathf.Abs (r) < Mathf.Abs (p / 2)) && !changedMat) {
					
					MeshRenderer mr = lnmNew.transpWall.GetComponent<MeshRenderer> ();
					Material[] mats = new Material[mr.materials.Length];
					lnmOld.transpWall.GetComponent<MeshRenderer> ().materials = lnmOld.origMaterials;

					for(int j = 0; j < mr.materials.Length; j++) {

						mats[j] = lnmNew.transpMaterial;
					}
					mr.materials = mats;
					changedMat = true;
				}

			}else{
				moving = false;

				i = 0f;
				k = 0f;

				if (numChild == 2) {
					go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, oldPos.transform.rotation.eulerAngles.y + dir * 180, go.transform.rotation.eulerAngles.z);
				} //else
					
					//go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, newPos.transform.rotation.eulerAngles.y, go.transform.rotation.eulerAngles.z);
				
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

				changedMat = false;
				if (CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
					selectButton.enabled = true;
					selectButton.GetComponent<Image> ().enabled = true;
					selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = true;
				}


				curPos = nextPos;
			}

		}

		if (darken) {
			j += Time.deltaTime * 1.25f;
			if (j < 1) {
				dlight.GetComponent<Light> ().intensity = Mathf.Lerp (startLightIntensity, endLightIntensity, j);
			} else {
				darken = false;
				j = 0f;
			}
		} else if (brighten) {
			j += Time.deltaTime * 1.25f;
			if (j < 1) {
				dlight.GetComponent<Light> ().intensity = Mathf.Lerp (endLightIntensity, startLightIntensity, j);
			} else {
				brighten = false;
				j = 0f;
			}
		}
	}
}
