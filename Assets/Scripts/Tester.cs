using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour
{


    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_1"),
                                              "time", 20,
                                              "easetype", iTween.EaseType.linear, 
                                              "oncomplete", "moveNext", 
                                              "oncompletetarget", this.gameObject));
    }

    void moveNext()
    {
        int route = Random.Range(1, 4);
        if (route == 1)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_2"),
                                                  "time", 20,
                                                   "easetype", iTween.EaseType.linear,
                                                   "oncomplete", "moveNext",
                                                   "oncompletetarget", this.gameObject));
        }
        if (route == 2)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_3"),
                                                  "time", 20,
                                                   "easetype", iTween.EaseType.linear,
                                                   "oncomplete", "moveNext",
                                                   "oncompletetarget", this.gameObject));
        }
        if (route == 3)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_4"),
                                                  "time", 25,
                                                   "easetype", iTween.EaseType.linear,
                                                   "oncomplete", "moveNext",
                                                   "oncompletetarget", this.gameObject));
        }
        if (route == 4)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_5"),
                                                  "time", 25,
                                                   "easetype", iTween.EaseType.linear,
                                                   "oncomplete", "moveNext",
                                                   "oncompletetarget", this.gameObject));
        }
    }

}

