using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Positioning : MonoBehaviour {

	static public bool isPositioning;
	static public GameObject placedElem;
	private static Transform parent = null;
	public ElementPlacing ep;
	private static Vector3 parentStartPos;
	private static float buttonStartY;

	void Start(){

		isPositioning = false;
		parent = ElementPlacing.CheckButton.transform.parent;
		parentStartPos = parent.position;
		buttonStartY = ElementPlacing.CheckButton.transform.localPosition.y;
	}

	public void checkPress(){

		if (isPositioning) {
			
			hideButtons ();
			if (!PlaySimulation.isSimActive) {
				if (placedElem.name.Contains ("ramp")) {
					BlockHover.showRampGrid ();
				} else
					BlockHover.showGrid ();
			}
			//BlockHover.hideRampGrid ();
			ep.canPlace = true;
			placedElem = null;
		}


	}

	static public void showButtons(){

		if (!isPositioning) {
			print (ElementPlacing.CheckButton.name);
			ElementPlacing.CheckButton.GetComponent<Button> ().interactable = true;
			ElementPlacing.LeftRotButton.GetComponent<Button> ().interactable = true;
			ElementPlacing.RightRotButton.GetComponent<Button> ().interactable = true;
			Positioning.parent = ElementPlacing.CheckButton.transform.parent;
			Positioning.parent.position = Camera.main.WorldToScreenPoint (placedElem.transform.position);
			if ((Camera.main.WorldToScreenPoint (placedElem.transform.position) + new Vector3 (0, ElementPlacing.CheckButton.GetComponent<RectTransform> ().rect.height * 2.5f, 0)).y > Screen.height){
				resetButtonPos ();

				ElementPlacing.CheckButton.transform.localPosition = new Vector3 (ElementPlacing.CheckButton.transform.localPosition.x, ElementPlacing.CheckButton.transform.localPosition.y - 150, ElementPlacing.CheckButton.transform.localPosition.z);
				ElementPlacing.LeftRotButton.transform.localPosition = new Vector3 (ElementPlacing.LeftRotButton.transform.localPosition.x, ElementPlacing.LeftRotButton.transform.localPosition.y - 150, ElementPlacing.LeftRotButton.transform.localPosition.z);
				ElementPlacing.RightRotButton.transform.localPosition = new Vector3 (ElementPlacing.RightRotButton.transform.localPosition.x, ElementPlacing.RightRotButton.transform.localPosition.y - 150, ElementPlacing.RightRotButton.transform.localPosition.z);

			
			}
			isPositioning = true;

		}
	}

	private static void resetButtonPos(){
		ElementPlacing.CheckButton.transform.localPosition = new Vector3 (ElementPlacing.CheckButton.transform.localPosition.x, buttonStartY, ElementPlacing.CheckButton.transform.localPosition.z);
		ElementPlacing.LeftRotButton.transform.localPosition = new Vector3 (ElementPlacing.LeftRotButton.transform.localPosition.x, buttonStartY, ElementPlacing.LeftRotButton.transform.localPosition.z);
		ElementPlacing.RightRotButton.transform.localPosition = new Vector3 (ElementPlacing.RightRotButton.transform.localPosition.x, buttonStartY, ElementPlacing.RightRotButton.transform.localPosition.z);

	}

	static public void hideButtons(){

		if (isPositioning) {
			ElementPlacing.CheckButton.GetComponent<Button> ().interactable = false;
			ElementPlacing.LeftRotButton.GetComponent<Button> ().interactable = false;
			ElementPlacing.RightRotButton.GetComponent<Button> ().interactable = false;
			Positioning.parent = ElementPlacing.CheckButton.transform.parent;
			Positioning.parent.position = Positioning.parentStartPos;
			resetButtonPos ();

			isPositioning = false;
		}
	}

	public void RotateRight(){
		if (placedElem.GetComponent<ElementProperties> ().rotatable) {

			placedElem.transform.rotation = Quaternion.Euler (new Vector3 (placedElem.transform.rotation.eulerAngles.x, placedElem.transform.rotation.eulerAngles.y + 90, placedElem.transform.rotation.eulerAngles.z));
	
		}
	}

	public void RotateLeft(){
		if (placedElem.GetComponent<ElementProperties> ().rotatable) {
			
			placedElem.transform.rotation = Quaternion.Euler (new Vector3 (placedElem.transform.rotation.eulerAngles.x, placedElem.transform.rotation.eulerAngles.y - 90, placedElem.transform.rotation.eulerAngles.z));

		}
	}

	public void RemoveElem(){


		if (placedElem.GetComponent<ElementProperties> ().removable) {
			ElementPlacing.currHold = ElementPlacing.truncateNumbers (placedElem.name);
			ElementPlacing.holding = true;
			if(placedElem.name.Contains("ramp")){
				BlockHover.showRampGrid();
				BlockHover.hideGrid();
			}else{
				BlockHover.showGrid ();
				BlockHover.hideRampGrid();
			}
			ep.decNum (placedElem.name);
			Destroy (placedElem);
			placedElem = null;
			ep.canPlace = true;

		}
		
		
	}

	void Update(){

		if (isPositioning && placedElem == null) {
			hideButtons ();
		} else if (isPositioning) {
			Positioning.parent = ElementPlacing.CheckButton.transform.parent;
			parent.position = Camera.main.WorldToScreenPoint (placedElem.transform.position);
			resetButtonPos ();
			if ((Camera.main.WorldToScreenPoint (placedElem.transform.position) + new Vector3 (0, ElementPlacing.CheckButton.GetComponent<RectTransform> ().rect.height * 2.5f, 0)).y > Screen.height) {

				ElementPlacing.CheckButton.transform.localPosition = new Vector3 (ElementPlacing.CheckButton.transform.localPosition.x, ElementPlacing.CheckButton.transform.localPosition.y - 150, ElementPlacing.CheckButton.transform.localPosition.z);
				ElementPlacing.LeftRotButton.transform.localPosition = new Vector3 (ElementPlacing.LeftRotButton.transform.localPosition.x, ElementPlacing.LeftRotButton.transform.localPosition.y - 150, ElementPlacing.LeftRotButton.transform.localPosition.z);
				ElementPlacing.RightRotButton.transform.localPosition = new Vector3 (ElementPlacing.RightRotButton.transform.localPosition.x, ElementPlacing.RightRotButton.transform.localPosition.y - 150, ElementPlacing.RightRotButton.transform.localPosition.z);

			}
		}

	}

}
