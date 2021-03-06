﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public GameObject GameOverUI_BG;
    public GameObject WarningMark;


    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float deathNumber;


	//Set difficulty
//	public float easyDifficulty =0.5f;
//	public float normalDifficulty = 1f;
//	public float hardDifficulty =2f;

    //[HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
       
    }
    


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
			FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        WarningMark.SetActive(false);
		        //TODO : Length Debugging
		if (deathNumber > 0) {
			deathNumber -= Time.deltaTime;
		} else {
			deathNumber = 0;
		}


		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);




		//Debug.Log("targetsInViewRadiusLength: " + targetsInViewRadius.Length);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics2D.Raycast (transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
					WarningMark.SetActive (true);
					visibleTargets.Add (target);
					GameOver ();
				} 
			}
        }
    }

    void GameOver()
    {
		if (SaveData.Difficulty == 0) {
			deathNumber += Time.deltaTime * 0.5f;
		} else if (SaveData.Difficulty == 1) {
			deathNumber += Time.deltaTime * 1;
		} else if (SaveData.Difficulty == 2) {
			deathNumber += Time.deltaTime * 2;
		}
		        
      


        if (deathNumber>=1.0){
          
            GameOverUI_BG.SetActive(true);
            Time.timeScale = 0;
            
        }
        

        
    }
   
   


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),  Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
