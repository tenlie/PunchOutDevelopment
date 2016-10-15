using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionMenu : MonoBehaviour {

    public Slider Volume = null;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = Volume.value;
	}


}
