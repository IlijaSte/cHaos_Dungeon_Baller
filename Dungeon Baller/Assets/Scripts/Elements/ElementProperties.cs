using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementProperties : MonoBehaviour {

	public bool rotatable = true;
	public bool removable = true;

    public bool rotating = false;
    public float rotSpeed = 5;

    private float i = 0f;
    private Vector3 newRot;
    private Vector3 startRot;

    public void rotate(float angleBy)
    {
        if (!rotating)
        {
            rotating = true;
            i = 0f;
            startRot = transform.rotation.eulerAngles;
            newRot = transform.rotation.eulerAngles + new Vector3(0, angleBy, 0);
        }
    }

    private void Update()
    {
        if (rotating)
        {
            i += Time.deltaTime * rotSpeed;
            if(i < 1)
            {
                transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, newRot, i));
            }
            else
            {
                rotating = false;
                transform.rotation = Quaternion.Euler(newRot);
            }
        }
    }
}
