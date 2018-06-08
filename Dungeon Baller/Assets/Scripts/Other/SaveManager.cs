using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager {

	[Serializable]
	public class SaveData
	{
		public int totalNumStars;
		public List<int>[] allCollected;
		public bool[] levelsPassed;

		public SaveData(){
			allCollected = new List<int>[256];
			levelsPassed = new bool[256];
			for(int i = 0; i < 256; i++)
				levelsPassed[i] = false;
		}
			
	}

	public static void saveGame()
	{
		
		if(!Directory.Exists(Application.persistentDataPath + "/save"))
			Directory.CreateDirectory (Application.persistentDataPath + "/save");
		FileStream fileStream = new FileStream(Application.persistentDataPath + "/save/save.sav", FileMode.Create);

		SaveData saveData = new SaveData();
		saveData.totalNumStars = CollectManager.totalNumCollected;

		for (int i = 0; i < 256; i++) {

			saveData.allCollected[i] = CollectManager.allCollected[i];
			saveData.levelsPassed [i] = CollectManager.levelsPassed [i];
		}
			
		//OVDE SE DODAJE JOS

		XmlSerializer serializer = new XmlSerializer (typeof(SaveData));

		serializer.Serialize (fileStream, saveData);
		fileStream.Close();
	}

	public static void loadGame()
	{

		if (Directory.Exists(Application.persistentDataPath + "/save") && File.Exists (Application.persistentDataPath + "/save/save.sav")) {
			FileStream fileStream = new FileStream (Application.persistentDataPath + "/save/save.sav", FileMode.Open);

			XmlSerializer serializer = new XmlSerializer (typeof(SaveData));
			SaveData saveData = serializer.Deserialize (fileStream) as SaveData;
			fileStream.Close ();


			CollectManager.totalNumCollected = saveData.totalNumStars;
		
			for (int i = 0; i < 256; i++) {
				CollectManager.allCollected [i] = saveData.allCollected [i];
				CollectManager.levelsPassed [i] = saveData.levelsPassed [i];
			
				//foreach(int j in CollectManager.allCollected[i])
				//	GameObject.Find ("Collectibles").transform.GetChild (j).GetComponent<CollectDetector> ().collected = true;

			}
			//OVDE SE DODAJE JOS

		} else {

			for (int i = 0; i < 256; i++) {
				CollectManager.allCollected [i] = new List<int> ();
				CollectManager.levelsPassed [i] = false;
			}
			CollectManager.levelsPassed [1] = true;
			CollectManager.levelsPassed [3] = true;
			CollectManager.levelsPassed [7] = true;
			//..

		}

	}


}