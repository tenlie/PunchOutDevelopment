using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionMenu : MonoBehaviour {

    public Slider Volume = null;
    //public  PlayerController PlayerController;
   // public Slider Sensitivity;
    //SaveData.PlayerSensitivity;

	// Use this for initialization
	void Start () {
        //SaveData.PlayerSensitivity = Sensitivity.value;

    }
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = Volume.value;
	}


}
