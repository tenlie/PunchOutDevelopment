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
	private bool finnished = false;
	public LevelManager lm;

	// Use this for initialization
	void Start () {
		startTime = lm.startTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (finnished)
			return;

		float t = Time.time - startTime-4;
		string minute = (5+(int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");

		timeText.text = minute + "시 " + seconds+"분";

	}
		
	public void Finnish()
	{
		finnished = true;
		timeText.color = Color.red;
	}

	//게임 끝나는 곳에 넣어줄 코드
	private void OnTriggerEnter(Collider other)
	{
		GameObject.Find ("player").SendMessage ("Finnish");
	}

}
