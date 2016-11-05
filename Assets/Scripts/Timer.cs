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

	public static string record, clearMessage ;

	public Text clearText;


	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		//spriteRenderer.sprite = Resources.Load<Sprite> ("Background/5pm")as Sprite;
	}

	// Update is called once per frame
	void Update () {

		float t = Time.time - startTime;
		if (finnished) {
			Finnish ();
			return;
		}
		string minute = (5+(int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");
		timeText.text = minute + "시 " + seconds+"분";
		record = timeText.text;

		//if you clear in 30min, 60min or over 60min
		if (t <= 30)
			clearMessage = "빠른 퇴근!!";
		else if (t <= 60)
			clearMessage = "나쁘지 않네요!";
		else if (t > 60)
			clearMessage = "회사가 집인지...";

		//BackgroundImage Changes
		if (minute+seconds == "530") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/6pm")as Sprite;
		else if(minute+seconds == "60") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/7pm")as Sprite;
		else if(minute+seconds == "630") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/8pm")as Sprite;


	}

	public void Finnish()
	{
		finnished = true;
		timeText.color = Color.red;
		clearText.text = record+"!! \r\n"+clearMessage;

		// 저장 시  minute + second  형태로 저장  ex) "1009"


	}



}
