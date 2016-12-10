using UnityEngine;
using System.Collections;

//타이머를 만들기 위해 끌어와야 하는 거
using UnityEngine.UI;


//For change string to int...
using System;
using System.Linq;
using System.Collections.Generic;

/// 이 클래스는 타이머 기능과 시간의 변화에 따른 배경화면 변화 기능을 갖고 있음.  - 우현
public class Timer : MonoBehaviour {

	public Text timeText;
	private float startTime;
	public bool finnished = false;

	public static string record, clearMessage, minute,seconds,rank;
	public Text clearText;


	public SpriteRenderer spriteRenderer;

	public SaveData saveData;
	public GameObject js;
	public PlayerController ps; 

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		//spriteRenderer.sprite = Resources.Load<Sprite> ("Background/5pm")as Sprite;
	}

	// Update is called once per frame
	void Update () {

		float t = Time.time - startTime;
		if (finnished) {
			Debug.Log("Update Finnish!");
			Finnish ();
			finnished = false;
			return;
		}


		if ((5 + (int)t / 60) < 10)
			minute = "0" + (5 + (int)t / 60);
		else
			minute = (5 + (int)t / 60).ToString();

		if ((t % 60) < 10)
			seconds = "0" + (t % 60).ToString("f0");
		else
			seconds = (t % 60).ToString("f0");

		//BackgroundImage Changes
		if (minute+seconds == "0530") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/6pm")as Sprite;
		else if(minute+seconds == "0600") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/7pm")as Sprite;
		else if(minute+seconds == "0630") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/8pm")as Sprite;
		else if(minute+seconds == "0700") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/9pm")as Sprite;

		timeText.text = minute + ":" + seconds;
		record = timeText.text;

		rank = "";
		//if you clear in 30min, 60min or over 60min
		if(t<=30)
			clearMessage = "빠른 퇴근!!";
		else if (t <= 60)
			clearMessage = "나쁘지 않네요!";
		else if (t > 60)
			clearMessage = "과로사...";
	}


	public void Finnish()
	{
	//	js.SetActive (false);
	//	ps.enabled = false;
		finnished = false;
		timeText.color = Color.white;
		clearText.text = record+"!! \r\n"+clearMessage;

		Debug.Log("Clear Time!!!!!!!  :::::::::::::::::::::::::::::::::::::::::::::: "+(minute+seconds));
		//SaveData.SaveHiScore (PlayerController.punchOutCnt + "", (minute + seconds));

		SaveData.LoadHiScore();
		if(SaveData.SaveHiScore(PlayerController.punchOutCnt+"",(minute+seconds)))
		{
			Debug.Log("SaveHiScore...");

			for (int i = 0; i < SaveData.HiScore.GetLength(0); i++)
			{
				Debug.Log ("HiScore[i,1]" + SaveData.HiScore [i, 1]);
				Debug.Log ("minute+seconds" + (minute + seconds));
				if (SaveData.HiScore [i, 1] == (minute + seconds))					
				{
					Debug.Log("My Rank!!!!");
					rank = (i+1)+"";
					clearText.text = clearText.text +"\r\n 역대급!! "+rank+"등";
				}
			}
		}
	}
}
