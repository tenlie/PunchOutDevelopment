using UnityEngine;
using UnityEngine.SceneManagement;

//For change string to int...
using System;
using System.Linq;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }
    const float SaveDataVersion = 0.1f;
    public static string SaveDate = "(non)";

    //static int[] HiScoreInitData = new int[10] { 300000, 100000, 75000, 50000, 25000, 10000, 7500, 5000, 2500, 1000 };
    //public static string[,] HiScore = new string[3,2] { {"1","0530" }, { "5", "0630" }, { "7", "0730" } };
    public static string[,] HiScore = new string[3, 2];

    // Option
    public static float SoundBGMVolume = 1.0f;
    public static float SoundSEVolume = 1.0f;
    public static float PlayerSensitivity = 30.0f;
    public static int Difficulty = 0;

    // Etc(Don't Save)
    public static bool continuePlay = false;
    public static int newRecord = -1;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < HiScore.GetLength(0); i++)
        {
            for (int j = 0; j < HiScore.GetLength(1); j++)
            {
                HiScore[i, j] = "";
            }
        }
    }

    static void SaveDataHeader(string dataGroupName)
    {
        PlayerPrefs.SetFloat("SaveDataVersion", SaveDataVersion);
        SaveDate = System.DateTime.Now.ToString("G");
        PlayerPrefs.SetString("SaveDataDate", SaveDate);
        PlayerPrefs.SetString(dataGroupName, "on");
    }

    static bool CheckSaveDataHeader(string dataGroupName)
    {
        if (!PlayerPrefs.HasKey("SaveDataVersion"))
        {
            Debug.Log("SaveData.CheckData : No Save Data");
            return false;
        }
        if (PlayerPrefs.GetFloat("SaveDataVersion") != SaveDataVersion)
        {
            Debug.Log("SaveData.CheckData : Version Error");
            return false;
        }
        if (!PlayerPrefs.HasKey(dataGroupName))
        {
            Debug.Log("SaveData.CheckData : No Group Data");
            return false;
        }
        SaveDate = PlayerPrefs.GetString("SaveDataDate");
        return true;
    }

    public static bool CheckGamePlayData()
    {
        return CheckSaveDataHeader("PunchOut_GamePlay");
    }

    public static bool SaveGamePlay()
    {
        try
        {
            Debug.Log("SaveData.SaveGamePlay : Start");

            // SaveDataInfo
            SaveDataHeader("PunchOut_GamePlay");
            { // PlayerData
                zFoxDataPackString playerData = new zFoxDataPackString();
                playerData.Add("Player_PunchOutCnt", PlayerController.punchOutCnt);
                /*
                playerData.Add("Player_HP", PlayerController.nowHp);
                playerData.Add("Player_Score", PlayerController.score);
                playerData.Add("Player_checkPointSceneName", PlayerController.checkPointSceneName);
                playerData.Add("Player_checkPointLabelName", PlayerController.checkPointLabelName);
                playerData.PlayerPrefsSetStringUTF8("PlayerData", playerData.EncodeDataPackString());
                */

                Debug.Log(playerData.EncodeDataPackString());
            }
            { // StageData
              //zFoxDataPackString stageData = new zFoxDataPackString();
              /*
              zFoxUID[] uidList = GameObject.Find("Stage").GetComponentsInChildren<zFoxUID>();
              foreach (zFoxUID uidItem in uidList)
              {
                  if (uidItem.uid != null && uidItem.uid != "(non)")
                  {
                      stageData.Add(uidItem.uid, true);
                  }
              }
              stageData.PlayerPrefsSetStringUTF8("StageData_" + Application.loadedLevelName, stageData.EncodeDataPackString());
              */
              //Debug.Log(stageData.EncodeDataPackString ());
            }
            { // EventData
                /*
                zFoxDataPackString eventData = new zFoxDataPackString();
                eventData.Add("Event_KeyItem_A", PlayerController.itemKeyA);
                eventData.Add("Event_KeyItem_B", PlayerController.itemKeyB);
                eventData.Add("Event_KeyItem_C", PlayerController.itemKeyC);
                eventData.PlayerPrefsSetStringUTF8("EventData", eventData.EncodeDataPackString());
                */
                //Debug.Log(playerData.EncodeDataPackString ());
            }
            // Save
            PlayerPrefs.Save();

            Debug.Log("SaveData.SaveGamePlay : End");
            return true;

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.SaveGamePlay : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static bool LoadGamePlay(bool allData)
    {
        try
        {
            // SaveDataInfo
            if (CheckSaveDataHeader("SDG_GamePlay"))
            {
                Debug.Log("SaveData.LoadGamePlay : Start");
                SaveDate = PlayerPrefs.GetString("SaveDataDate");
                if (allData)
                { // PlayerData
                    zFoxDataPackString playerData = new zFoxDataPackString();
                    playerData.DecodeDataPackString(playerData.PlayerPrefsGetStringUTF8("PlayerData"));
                    Debug.Log(playerData.PlayerPrefsGetStringUTF8("PlayerData"));

                    PlayerController.punchOutCnt = (int)playerData.GetData("Player_PunchOutCnt");
                    /*
                    PlayerController.nowHp = (float)playerData.GetData("Player_HP");
                    PlayerController.score = (int)playerData.GetData("Player_Score");
                    PlayerController.checkPointEnabled = (bool)playerData.GetData("Player_checkPointEnabled");
                    PlayerController.checkPointSceneName = (string)playerData.GetData("Player_checkPointSceneName");
                    PlayerController.checkPointLabelName = (string)playerData.GetData("Player_checkPointLabelName");
                    */
                }
                // StageData
                if (PlayerPrefs.HasKey("StageData_" + SceneManager.GetActiveScene().name))
                {
                    //zFoxDataPackString stageData = new zFoxDataPackString();
                    //stageData.DecodeDataPackString(stageData.PlayerPrefsGetStringUTF8("StageData_" + SceneManager.GetActiveScene().name));
                    //Debug.Log(stageData.PlayerPrefsGetStringUTF8 ("StageData_" + Application.loadedLevelName));

                    /*
                    zFoxUID[] uidList = GameObject.Find("Stage").GetComponentsInChildren<zFoxUID>();
                    foreach (zFoxUID uidItem in uidList)
                    {
                        if (uidItem.uid != null && uidItem.uid != "(non)")
                        {
                            if (stageData.GetData(uidItem.uid) == null)
                            {
                                uidItem.gameObject.SetActive(false);
                            }
                        }
                    }
                    */

                }
                if (allData)
                { // EventData

                    //zFoxDataPackString eventData = new zFoxDataPackString();
                    //eventData.DecodeDataPackString(eventData.PlayerPrefsGetStringUTF8("EventData"));
                    //Debug.Log(playerData.PlayerPrefsGetStringUTF8 ("PlayerData"));

                    /*
                    PlayerController.itemKeyA = (bool)eventData.GetData("Event_KeyItem_A");
                    PlayerController.itemKeyB = (bool)eventData.GetData("Event_KeyItem_B");
                    PlayerController.itemKeyC = (bool)eventData.GetData("Event_KeyItem_C");
                    */
                }
                Debug.Log("SaveData.LoadGamePlay : End");
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.LoadGamePlay : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static string LoadContinueSceneName()
    {
        if (CheckSaveDataHeader("SDG_GamePlay"))
        {
            zFoxDataPackString playerData = new zFoxDataPackString();
            playerData.DecodeDataPackString(playerData.PlayerPrefsGetStringUTF8("PlayerData"));
            return (string)playerData.GetData("Player_checkPointSceneName");
        }

        continuePlay = false;
        return "StageA";
    }

    // punchoutcnt - playerControl에 있음.
    // playerSocre - Timer 에 있음.

    public static bool SaveHiScore(string punchOutCnt, string playerScore)
    {

        LoadHiScore();

        //if rank high score... 

        try
        {
            Debug.Log("SaveData.SaveHiScore : Start");
            // Hiscore Set & Sort
            newRecord = 0;
            string[,] scoreList = new string[HiScore.Length + 1, 2];
            HiScore.CopyTo(scoreList, 0);
            scoreList[scoreList.Length - 1, 0] = punchOutCnt;
            scoreList[scoreList.Length - 1, 1] = playerScore;

            //Sort ScoreList
            for (int i = 0; i < scoreList.Length - 1; i++)
            {
                for (int j = 1; j < scoreList.Length - i; j++)
                {
                    if (Convert.ToInt32(scoreList[j - 1, 1]) < Convert.ToInt32(scoreList[j, 1]))
                    {
                        swap(scoreList, j);
                    }
                }
            }
            
            // Hiscore Save
            SaveDataHeader("SDG_HiScore");
            zFoxDataPackString hiscoreData = new zFoxDataPackString();
            for (int i = 0; i < HiScore.Length; i++)
            {
                hiscoreData.Add("PunchOutCntforRank" + (i + 1), HiScore[i, 0]);
                hiscoreData.Add("HiScoreforRank" + (i + 1), HiScore[i, 1]);
            }
            hiscoreData.PlayerPrefsSetStringUTF8("HiScoreData", hiscoreData.EncodeDataPackString());

            PlayerPrefs.Save();
            Debug.Log("SaveData.SaveHiScore : End");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.SaveHiScore : Failed (" + e.Message + ")");
        }

        return false;
    }

    public static void swap(string[,] scorelist, int i)
    {
        string tempCnt = scorelist[i - 1, 0];
        string tempScore = scorelist[i - 1, 1];

        scorelist[i - 1, 0] = scorelist[i, 0];
        scorelist[i - 1, 1] = scorelist[i, 1];

        scorelist[i, 0] = tempCnt;
        scorelist[i, 1] = tempScore;
    }

    public static bool LoadHiScore()
    {
        try
        {
            if (CheckSaveDataHeader("SDG_HiScore"))
            {
                Debug.Log("SaveData.LoadHiScore : Start");
                zFoxDataPackString hiscoreData = new zFoxDataPackString();
                hiscoreData.DecodeDataPackString(hiscoreData.PlayerPrefsGetStringUTF8("HiScoreData"));
                Debug.Log(hiscoreData.PlayerPrefsGetStringUTF8("HiScoreData"));
                for (int i = 0; i < HiScore.Length; i++)
                {
                    HiScore[i,0] = (string)hiscoreData.GetData("PunchOutCntforRank" + (i + 1));
                    HiScore[i,1] = (string)hiscoreData.GetData("HiScoreforRank" + (i + 1));
                }
                Debug.Log("SaveData.LoadHiScore : End");
            }
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.LoadHiScore : Failed (" + e.Message + ")");
        }
        return false;
    }

    // === コード（オプションデータ・セーブロード） ================
    public static bool SaveOption()
    {
        try
        {
            Debug.Log("SaveData.SaveOption : Start");
            // Option Data
            SaveDataHeader("SDG_Option");

            PlayerPrefs.SetFloat("SoundBGMVolume", SoundBGMVolume);
            PlayerPrefs.SetFloat("SoundSEVolume", SoundSEVolume);
            PlayerPrefs.SetFloat("PlayerSensitivity", PlayerSensitivity);
            PlayerPrefs.SetInt("Difficulty", Difficulty);

            // Save
            PlayerPrefs.Save();
            Debug.Log("SaveData.SaveOption : End");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.SaveOption : Failed (" + e.Message + ")");
        }
        return false;
    }

    public static bool LoadOption()
    {
        try
        {
            if (CheckSaveDataHeader("SDG_Option"))
            {
                Debug.Log("SaveData.LoadOption : Start");

                SoundBGMVolume = PlayerPrefs.GetFloat("SoundBGMVolume");
                SoundSEVolume = PlayerPrefs.GetFloat("SoundSEVolume");
                PlayerSensitivity = PlayerPrefs.GetFloat("PlayerSensitivity");
                Difficulty = PlayerPrefs.GetInt("Difficulty");

                Debug.Log("SoundBGMVolume : " + SoundBGMVolume);
                Debug.Log("SoundSEVolume : " + SoundSEVolume);
                Debug.Log("PlayerSensitivity : " + PlayerSensitivity);
                Debug.Log("Difficulty : " + Difficulty);
                Debug.Log("SaveData.LoadOption : End");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("SaveData.LoadOption : Failed (" + e.Message + ")");


        }

        Debug.Log("SoundBGMVolume : " + SoundBGMVolume);
        Debug.Log("SoundSEVolume : " + SoundSEVolume);
        Debug.Log("PlayerSensitivity : " + PlayerSensitivity);
        Debug.Log("Difficulty : " + Difficulty);

        return false;
    }

    // === コード（セーブロードの削除・初期化） ==---==============
    public static void DeleteAndInit(bool init)
    {
        Debug.Log("SaveData.DeleteAndInit : DeleteAll");
        PlayerPrefs.DeleteAll();

        if (init)
        {
            Debug.Log("SaveData.DeleteAndInit : Init");
            SaveDate = "(non)";
            SoundBGMVolume = 1.0f;
            SoundSEVolume = 1.0f;

            //            HiScoreInitData.CopyTo(HiScore, 0);
        }
    }
}

// Windows
// (ComputerName)\HKEY_CURRENT_USR\Software\(CompanyName:DefaultCompany)\(AppName)
// http://docs-jp.unity3d.com/Documentation/ScriptReference/PlayerPrefs.html


