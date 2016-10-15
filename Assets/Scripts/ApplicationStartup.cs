using UnityEngine;
using System.Collections;

public class ApplicationStartup : MonoBehaviour {
	void Start () {
		Debug.Log ("=== Application Startup [Punch Out] ===");
		SaveData.LoadOption ();
	}
}
