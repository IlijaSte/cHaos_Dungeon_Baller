using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	[SerializeField]
	public GameObject[] cloudPrefabs;
	private float i = 0;
	public Transform cloudSpace;
	public float spawnSpeed = 0;

	void Start () {
		// namestanje klice
		if (spawnSpeed == 0) {
			Random.InitState ((int)System.DateTime.Now.Ticks);
			spawnSpeed = Random.Range (2, 4);
		}
		if (spawnSpeed == -1)
			spawnSpeed = 0;

		// stvaranje prvobitnih oblaka u vidnom prostoru

		int j = 2;
		if (Random.Range (0, 2) == 1) {
			j++;
		}
		for (int i = 0; i < j; i++) {
			GameObject newCloud = Instantiate (cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)]);

			Vector3 x = Quaternion.AngleAxis (cloudSpace.rotation.eulerAngles.y, Vector3.up) * new Vector3 (Random.Range (0, cloudSpace.localScale.x) - cloudSpace.localScale.x / 2, 0, 0);

			newCloud.transform.position = Vector3.Lerp (cloudSpace.position, ((Random.Range (0, 2) == 0) ? cloudSpace.position + x : cloudSpace.position - x), Random.Range (0f, 1f));
			Vector3 z = Quaternion.AngleAxis (cloudSpace.rotation.eulerAngles.y, Vector3.up) * new Vector3 (0, 0, Random.Range (0, cloudSpace.localScale.z) - cloudSpace.localScale.z / 2);

			newCloud.transform.position = Vector3.Lerp(newCloud.transform.position, ((Random.Range(0, 2) == 0) ? newCloud.transform.position + z:newCloud.transform.position - z), Random.Range(0f, 1f));
			newCloud.transform.position = new Vector3 (newCloud.transform.position.x, cloudSpace.position.y + Random.Range (0, cloudSpace.localScale.y) - cloudSpace.localScale.y / 2, newCloud.transform.position.z);
			//newCloud.transform.localScale = new Vector3 (Random.Range (8, 12) * 5, Random.Range (1, 3) * 5, Random.Range (6, 8) * 5);
			//newCloud.transform.rotation = Quaternion.Euler(cloudSpace.rotation.eulerAngles + new Vector3(0, 90, 0));
			newCloud.transform.rotation = Quaternion.Euler(cloudSpace.rotation.eulerAngles + new Vector3(0, 0, -90));
			float cloudSpeed = Random.Range (10f, 15f);

			newCloud.GetComponent<CloudMovement> ().speed = new Vector3(0, cloudSpeed, 0);
			//newCloud.GetComponent<CloudMovement> ().speed = new Vector3(x1, 0, y1);
		}

	}
	
	void Update () {

		i += Time.deltaTime * spawnSpeed;

		// stvaranje oblaka
		if (i >= 25) {


			GameObject newCloud = Instantiate (cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)]);

			Vector3 x = Quaternion.AngleAxis (cloudSpace.rotation.eulerAngles.y, Vector3.up) * new Vector3 (cloudSpace.localScale.x / 2, 0, 0);

			newCloud.transform.position = cloudSpace.position - x;
			Vector3 z = Quaternion.AngleAxis (cloudSpace.rotation.eulerAngles.y, Vector3.up) * new Vector3 (0, 0, Random.Range (0, cloudSpace.localScale.z) - cloudSpace.localScale.z / 2);

			newCloud.transform.position = Vector3.Lerp(newCloud.transform.position, ((Random.Range(0, 1) == 0) ? newCloud.transform.position + z:newCloud.transform.position - z), Random.Range(0f, 1f));
			newCloud.transform.position = new Vector3 (newCloud.transform.position.x, cloudSpace.position.y + Random.Range (0, cloudSpace.localScale.y) - cloudSpace.localScale.y / 2, newCloud.transform.position.z);
			//newCloud.transform.localScale = new Vector3 (Random.Range (8, 12) * 5, Random.Range (1, 3) * 5, Random.Range (6, 8) * 5);
			newCloud.transform.rotation = Quaternion.Euler(cloudSpace.rotation.eulerAngles + new Vector3(0, 0, -90));
			float cloudSpeed = Random.Range (5f, 8f);

			newCloud.GetComponent<CloudMovement> ().speed = new Vector3(0, cloudSpeed, 0);
			//newCloud.GetComponent<CloudMovement> ().speed = new Vector3(x1, 0, y1);
			spawnSpeed = Random.Range(2, 4);
			i = 0f;
		}

	}
}
