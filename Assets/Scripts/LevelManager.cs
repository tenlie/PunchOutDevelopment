using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set; }
    public PlayerController playerController;

    public bool isPaused { get; set; }
    public bool isGameClear { get; set; }
    public bool isGameOver { get; set; }

    public TimeSpan RunningTime { get { return DateTime.UtcNow - started; } }
    private DateTime started;

    public GameObject ReadyStartUI;
    public GameObject GameOverUI_BG;
    public GameObject PauseUI_BG;
    public GameObject ClearUI_BG;

    public GameObject Joystick;

    public Text ReadyStartText;

	public static float startTime;
	public GameObject TimerUI;

    void Awake()
    {
        Debug.Log("LevelManager >>> Awake()");

        Instance = this;
        isPaused = false;
        isGameClear = false;
        isGameOver = false;

        

        ReadyStartUI.transform.localPosition = new Vector3(0, 0, 0);
        ClearUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        GameOverUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        PauseUI_BG.transform.localPosition = new Vector3(0, 0, 0);
        Joystick.transform.position = new Vector3(0, 0, 0);

        ReadyStartUI.SetActive(false);
        ClearUI_BG.SetActive(false);
        GameOverUI_BG.SetActive(false);
        PauseUI_BG.SetActive(false);
        Joystick.SetActive(false);
		TimerUI.SetActive (false);
    }

	void Start ()
    {
        Debug.Log("LevelManager >>> Start()");

        //started = DateTime.UtcNow;
        StartCoroutine("ReadyStartWork");

	}

    IEnumerator ReadyStartWork()
    {
        Debug.Log("LevelManager >>> ReadyStartWork()");

        zFoxFadeFilter.instance.FadeIn(Color.black, 0.1f);
        ReadyStartUI.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        ReadyStartText.text = "Start!";
        yield return new WaitForSeconds(2.0f);
        ReadyStartUI.SetActive(false);
        Joystick.SetActive(true);


		TimerUI.SetActive (true);
		startTime = Time.time;


        yield break;
    }

    void OnApplicationPause(bool pause)
    {
        PauseApplication(pause);
    }

    public void PauseApplication(bool pause)
    {
        if (pause)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Debug.Log("LevelManager >>> PauseGame()");

        if (isGameClear || isGameOver)
        {
            return;
        }

        isPaused = true;
        PauseUI_BG.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Debug.Log("LevelManager >>> ResumeGame()");

        isPaused = false;
        PauseUI_BG.SetActive(false);
        Time.timeScale = 1;
    }


    public void ReturnToMain()
    {
        Debug.Log("LevelManager >>> ReturnToMain()");

        SaveData.SaveOption();
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
        
    }

    public void RestartGame()
    {
        Debug.Log("LevelManager >>> RestartGame()");

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        playerController.Kill();
        yield return new WaitForSeconds(2f);
        //Open GameOver Popup     
        GameOverUI_BG.SetActive(true);
    }

    public void StartTimer()
    {

    }
}
