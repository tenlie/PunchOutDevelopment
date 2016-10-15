using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour
{

    //int route = Random.Range(1, 5);


    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_1"),
                                              "time", 10,
                                              "easetype", iTween.EaseType.linear, 
                                              "oncomplete", "moveNext", 
                                              "oncompletetarget", this.gameObject));
    }

    void moveNext()
    {
        int route = 1;
        if (route == 1)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_2"),
                                                  "time", 10,
                                                  "easetype", iTween.EaseType.linear));
        }
    }

}

