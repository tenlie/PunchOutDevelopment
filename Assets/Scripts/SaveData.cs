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

    // 이렇게 계속 초기화 시키면 실행할때마다 초기화 되는거 아닐까?
    public static string[,] HiScore = new string[3,2] { {"0","9999" }, { "0", "9999" }, { "0", "9999" } };

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
		Debug.Log ("SaveDataClass come?");
		Debug.Log ("playerScore : "+playerScore +" punchOutCnt : "+ punchOutCnt);
		Debug.Log ("HiScore[0,0]"+HiScore[0,0]);

        LoadHiScore();
        //i기존 기록 3등 안에 들었을 경우 넣어주기!
       // int ranking = 0 ; // 갱신된 순위 알려주는 거
        bool flag = false;
		Debug.Log ("HiScore.GetLength(0)"+HiScore.GetLength(0));
		for(int i = 1; i<HiScore.GetLength(0); i++)
        {
			Debug.Log ("for(int i = 1; i<HiScore.GetLength(0); i++) -> "+i);

			if( Convert.ToInt32(HiScore[i,1])  >  Convert.ToInt32(playerScore))
			{
                flag = true;
            }
        }
		Debug.Log ("flag : " + flag);
        // 기존 기록보다 좋지 않은 경우엔 아래 코드들을 실행시키지 않고 리턴 시켜준다.
        if(flag==false)
        {
			Debug.Log ("flag : "+flag);
            return false  ;
        }

        try
        {
            Debug.Log("SaveData.SaveHiScore : Start");
            // Hiscore Set & Sort
            newRecord = 0;
			Debug.Log("Before HiScore.Length");
			string[,] scoreList = new string[4,2] { {"0","9998" },{"0","9998" }, { "0", "9998" }, { "0", "9998" } };;
			Debug.Log("After HiScore.Length");
            

			//HiScore.CopyTo(scoreList, 0);

			for(int i= 0 ; i < HiScore.GetLength(0); i++)
			{
				Debug.Log("before : scoreList[i] : "+scoreList[i,1].ToString());
				scoreList[i,0] = HiScore[i,0];
				scoreList[i,1] = HiScore[i,1];

				Debug.Log("after : scoreList[i] : "+scoreList[i,1].ToString());
			}


			Debug.Log("After HiScore.CopyTo(scoreList,0)");

			scoreList[scoreList.GetLength(0) - 1, 0] = punchOutCnt;
			scoreList[scoreList.GetLength(0) - 1, 1] = playerScore;

			Debug.Log("Before Sort ScoreList");

			for(int i = 0 ; i<scoreList.GetLength(0); i++)
			{
				Debug.Log("scoreList[i,1] : "+scoreList[i,1]);
			}

			//Sort ScoreList
			for (int i = 0; i < scoreList.GetLength(0) - 1; i++)
			{
				for (int j = 1; j < scoreList.GetLength(0) - i; j++)
				{
					if (Convert.ToInt32(scoreList[j - 1, 1]) > Convert.ToInt32(scoreList[j, 1]))
					{
						swap(scoreList, j);
					}
				}
			}

			Debug.Log("After Sort ScoreList");
			for(int i = 0 ; i<scoreList.GetLength(0); i++)
			{
				Debug.Log("scoreList[i,1] : "+scoreList[i,1]);
			}

			for(int i= 0 ; i < HiScore.GetLength(0); i++)
			{
				Debug.Log("before : HiScore[i] : "+HiScore[i,1].ToString());
				HiScore[i,0] = scoreList[i,0];
				HiScore[i,1] = scoreList[i,1];

				Debug.Log("after : HiScore[i] : "+HiScore[i,1].ToString());
			}


            // Hiscore Save
			SaveDataHeader("SDG_HiScore");
            zFoxDataPackString hiscoreData = new zFoxDataPackString();
            for (int i = 0; i < HiScore.GetLength(0); i++)
            {
                hiscoreData.Add("PunchOutCntforRank" + (i + 1), HiScore[i, 0]);
                hiscoreData.Add("HiScoreforRank" + (i + 1), HiScore[i, 1]);
            }
            hiscoreData.PlayerPrefsSetStringUTF8("HiScoreData", hiscoreData.EncodeDataPackString());

            PlayerPrefs.Save();
			LoadHiScore();
			for(int i = 0 ; i<HiScore.GetLength(0); i++)
			{
				Debug.Log("HiScore[i,1] : "+HiScore[i,1]);
			}

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
		Debug.Log ("LoadHiScore Come");
        try
        {
            if (CheckSaveDataHeader("SDG_HiScore"))
            {
                Debug.Log("SaveData.LoadHiScore : Start");
                zFoxDataPackString hiscoreData = new zFoxDataPackString();
                hiscoreData.DecodeDataPackString(hiscoreData.PlayerPrefsGetStringUTF8("HiScoreData"));
                Debug.Log(hiscoreData.PlayerPrefsGetStringUTF8("HiScoreData"));
                for (int i = 0; i < HiScore.GetLength(0); i++)
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
			Debug.Log ("LoadHiScore catch");
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
        Debug.Log("asdf");
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


