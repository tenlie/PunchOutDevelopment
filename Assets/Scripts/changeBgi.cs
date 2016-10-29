using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class changeBgi : MonoBehaviour {


	public Text clearText;
	public SpriteRenderer spriteRenderer; 


	void Start() 
	{ 
		spriteRenderer.sprite = Resources.Load<Sprite> ("Background/5pm")as Sprite;
	} 


	void Update()
	{
		Debug.Log ("시간"+clearText);
		if (clearText.ToString() == "6시 1분") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/6pm")as Sprite;
		else if(clearText.ToString() == "7시 1분") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/7pm")as Sprite;
		else if(clearText.ToString() =="8시 1분") spriteRenderer.sprite = Resources.Load<Sprite> ("Background/8pm")as Sprite;
	} 
}
