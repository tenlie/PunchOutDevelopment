using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject OptionUI_BG;
    private bool isOptionUIOpen;
    public Slider BGMSlider;
    public Slider SensitivitySlider;
    public Dropdown DifficultyDropdown;

    public GameObject RankingUI_BG;
    private bool isRankingUIOpen;
    public Text Rank1;
    public Text Rank2;
    public Text Rank3;

    public GameObject CreditUI_BG;
    private bool isCregitUIOpen;

    public AudioSource BGM;

    private string jumpSceneName;

    private bool isPaused;

    void Start()
    {
        Debug.Log("StartScreen >>> Start()");

        System.GC.Collect();
        zFoxFadeFilter.instance.FadeIn(Color.black, 1.0f);

        isPaused = false;

        isOptionUIOpen = false;
        isRankingUIOpen = false;
        isCregitUIOpen = false;

        OptionUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        OptionUI_BG.SetActive(false);
        RankingUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        RankingUI_BG.SetActive(false);
        CreditUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        CreditUI_BG.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        BGM.Pause();
    }

    public void ResumeGame()
    {
        isPaused = false;
        BGM.Play();
    }

    void Button_Play(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Play()");

        if (isOptionUIOpen)
        {
            return;
        }

        BGM.Stop();
        zFoxFadeFilter.instance.FadeOut(Color.white, 1.0f);
        jumpSceneName = "GameStage";
        Invoke("SceneJump", 1.2f);
    }

    void Button_Option(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Option()");

        if (isOptionUIOpen)
        {
            return;
        }

        //옵션화면 활성화
        isOptionUIOpen = true;
        Time.timeScale = 0;
        OptionUI_BG.SetActive(true);
        SetOptionData();

    }

    public void Close_OptionUI()
    {
        Debug.Log("StartScreen >>> Close_OptionUI()");

        isOptionUIOpen = false;
        Time.timeScale = 1;
        OptionUI_BG.SetActive(false);
        SaveOptionData();
    }

    void SceneJump()
    {
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

    void Button_Ranking(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Ranking()");

        SetRankingData();
        RankingUI_BG.SetActive(true);
    }

    public void SetRankingData()
    {
        Debug.Log("StartScreen >>> SetRankingData()");
        SaveData.LoadHiScore();
        Debug.Log("SaveData.HiScore.Length: " + SaveData.HiScore.GetLength(0));

        for (int i = 0; i < SaveData.HiScore.GetLength(0); i++)
        {
            Debug.Log("Ranking" + (i + 1) + ">>> Score: " + SaveData.HiScore[i, 1].Substring(0, 2) + "시" + SaveData.HiScore[i, 1].Substring(2, 2) 
                + "분 PunchOutCnt: " + SaveData.HiScore[i, 0]);
        }

        Rank1.text = SaveData.HiScore[0, 1].Substring(0, 2) + "시" + SaveData.HiScore[0, 1].Substring(2, 2) + "분    " + SaveData.HiScore[0, 0];
        Rank2.text = SaveData.HiScore[1, 1].Substring(0, 2) + "시" + SaveData.HiScore[1, 1].Substring(2, 2) + "분    " + SaveData.HiScore[1, 0];
        Rank3.text = SaveData.HiScore[2, 1].Substring(0, 2) + "시" + SaveData.HiScore[2, 1].Substring(2, 2) + "분    " + SaveData.HiScore[2, 0];
    }

    public void Close_RankingUI()
    {
        Debug.Log("StartScreen >>> Close_RankingUI()");

        RankingUI_BG.SetActive(false);
    }

    void Button_Credit(MenuObject_Button button)
    {
        Debug.Log("StartScreen >>> Button_Credit()");
        SetOptionData();
        CreditUI_BG.SetActive(true);
    }

    public void Close_CreditUI()
    {
        CreditUI_BG.SetActive(false);
    }
}
