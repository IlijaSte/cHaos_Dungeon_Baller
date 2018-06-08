using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullInventory : MonoBehaviour {

	AvailElemManager availManager;

	void Awake(){
		availManager = (AvailElemManager)GameObject.Find ("InvPanel").GetComponent("AvailElemManager");

	}

	public void getFullInv(){

		availManager.availElements.Clear ();
		availManager.availElements.Add (new AvailElemManager.AvailElement (availManager.block, 100, "block"));
		availManager.availElements.Add (new AvailElemManager.AvailElement (availManager.setdir, 100, "setdir"));
		availManager.availElements.Add (new AvailElemManager.AvailElement (availManager.ramp, 100, "ramp"));
		availManager.availElements.Add (new AvailElemManager.AvailElement (availManager.curve, 100, "curve"));
		availManager.availElements.Add (new AvailElemManager.AvailElement (availManager.pistonBlock, 100, "pistonblock"));

	}
}
