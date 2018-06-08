using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvOpen : MonoBehaviour {

	public bool open = false;
	public GameObject panel;
	public GameObject canvas;
	private RectTransform rt;

	Vector2 initClickPos;
	float i = 0;
	float j = 0;
	bool opening = false, closing = false;
	int dir = -1;
	Vector3 endPos;
	Vector3 startPos;
	Transform arrow = null;
	Vector3 arrowOpen;
	Vector3 arrowClose;

	void Start(){
		
		arrow = panel.transform.Find ("InventoryArrow");
		arrowOpen = arrow.transform.rotation.eulerAngles;
		arrowClose = arrow.transform.rotation.eulerAngles + new Vector3 (0, 0, 180);
		panel.transform.Find ("ButtonScroll").GetComponent<ScrollRect> ().verticalNormalizedPosition = 1;
	}

	void Update(){

		if (Input.GetMouseButtonDown (0)) {

			initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			i = 0;
		} else if (Input.GetMouseButtonUp (0)) {

			i = 0;
		}
		if (!opening && !closing) {

			if (Input.GetMouseButton (0)) {

				float deltaX = initClickPos.x - Input.mousePosition.x;
				print (i);
				if (deltaX > 0) {

					if (dir == 1) {
						i = 0f;
						dir = -1;
						initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						i += deltaX;
					if (i > 100f) {

						i = 0f;
						dir = -1;
						if (!open) {
							opening = true;
							startPos = panel.transform.position;
							endPos = panel.transform.position - panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor);
						}

					}

				} else if (deltaX < 0) {
					if (dir == -1) {
						i = 0;
						dir = 1;

						initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						i += deltaX;
					if (i < -100f) {


						i = 0f;
						dir = 1;
						if (open) {
							closing = true;
							//opening = true;
							startPos = panel.transform.position;
							endPos = panel.transform.position + panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor);
						}

					}


				}
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

			}

		}else

		if (opening) {

			j += 3 * Time.deltaTime;
			if (j < 1) {
					arrow.rotation = Quaternion.Euler(Vector3.Lerp(arrowOpen, arrowClose, j));
					panel.transform.position = Vector3.Lerp (startPos, endPos,j);
			} else {
				opening = false;
				open = true;
				j = 0f;
				i = 0f;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			}

		} else if (closing) {
			j += 3 * Time.deltaTime;
			if (j < 1) {
				arrow.rotation = Quaternion.Euler(Vector3.Lerp(arrowClose, arrowOpen, j));
				panel.transform.position = Vector3.Lerp (startPos, endPos, j);
			} else {
				closing = false;
				open = false;
				j = 0f;
				i = 0f;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			}
		}


	}

	public void MoveInvPanel(){
		rt = canvas.GetComponent<RectTransform> ();
		if (!open) {
			
			open = true;

			panel.transform.Translate (-panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor));
		} else {

			open = false;
			panel.transform.Translate (panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor));

		}

	}
		
}
