using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    public GameObject OptionUI_BG;
    public Slider BGMSlider;
    public Slider SensitivitySlider;
    public Dropdown DifficultyDropdown;
    private bool isOptionOpen;


    private string jumpSceneName;
    

	void Start ()
    {
        Debug.Log("StartScreen >>> Start()");

        System.GC.Collect();
        zFoxFadeFilter.instance.FadeIn(Color.black, 1.0f);
        isOptionOpen = false;

        OptionUI_BG.transform.localPosition = new Vector3(0,0,0);
        OptionUI_BG.SetActive(false);
    }

    void Button_Play(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Play()");

        if (isOptionOpen)
        {
            return;
        }

        zFoxFadeFilter.instance.FadeOut(Color.white, 1.0f);
        jumpSceneName = "GameStage";
        Invoke("SceneJump", 1.2f);
    }

    void Button_Option(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Option()");

        if (isOptionOpen)
        {
            return;
        }

        //옵션화면 활성화
        isOptionOpen = true;
        Time.timeScale = 0;
        OptionUI_BG.SetActive(true);

        SetOptionData();


    }

    public void Close_OptionUI()
    {
        Debug.Log("StartScreen >>> Close_OptionUI()");

        isOptionOpen = false;
        Time.timeScale = 1;
        OptionUI_BG.SetActive(false);
        SaveOptionData();
    }

    void SceneJump() {
        Debug.Log(string.Format("Start Game : {0}", jumpSceneName));
        SceneManager.LoadScene(jumpSceneName);
    }

    public void SetOptionData()
    {
        BGMSlider.value = SaveData.SoundBGMVolume;
        SensitivitySlider.value = SaveData.PlayerSensitivity;
        DifficultyDropdown.value = SaveData.Difficulty;
    }

    public void SaveOptionData()
    {
        SaveData.SoundBGMVolume = BGMSlider.value;
        SaveData.PlayerSensitivity = SensitivitySlider.value;
        SaveData.Difficulty = DifficultyDropdown.value;

        SaveData.SaveOption();
    }

}
