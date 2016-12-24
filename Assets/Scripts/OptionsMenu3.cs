using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu3 : MonoBehaviour
{

    public Slider Volume = null;
    public Slider Sensitivity = null;
    public PlayerController PlayerController = null;
    public LevelManager LevelManager;
    // public Slider Sensitivity;
    //SaveData.PlayerSensitivity;

    // Use this for initialization
    void Start()
    {
        //SaveData.PlayerSensitivity = Sensitivity.value;

    }

    // Update is called once per frame
    void Update()
    {
		AudioListener.volume = Volume.value;
        PlayerController.moveSpeed = Sensitivity.value;
        LevelManager.SaveOptionData();
        SaveData.LoadOption();
        LevelManager.SetOptionData();
        //LevelManager.SetOptionData();
    }


}
