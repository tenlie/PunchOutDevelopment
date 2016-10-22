using UnityEngine;
using System.Collections;

//타이머를 만들기 위해 끌어와야 하는 거 
using UnityEngine.UI;


//For change string to int...
using System;
using System.Linq;
using System.Collections.Generic;


public class Timer : MonoBehaviour {

	public Text timeText;
	private float startTime; 
	public bool finnished = false;

	public static string record, clearMessage ;

	public Text clearText;


	// Use this for initialization
	void Start () {
		startTime = Time.time;
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
			clearMessage = "회사가 집인지 \r\n 집이 회사인지";
	}
		
	public void Finnish()
	{
		finnished = true;
		timeText.color = Color.red;

		clearText.text = record+"!! \r\n"+clearMessage;
	}



}
