using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour
{

    void Start()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        int routeA = Random.Range(1, 2);
        if (routeA == 1)
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("Path_1"),
                                              "time", 100,
                                              "easetype", iTween.EaseType.linear,
                                              //"oncomplete", "MoveToBack",
                                              "looptype", iTween.LoopType.pingPong));
        }
        if (routeA == 2)
        {
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPathReversed("Path_1"),
                                              "time", 100,
                                              "easetype", iTween.EaseType.linear,
                                              //"oncomplete", "MoveToBack",
                                              "looptype", iTween.LoopType.pingPong
                                              //"oncompleterget", this.gameObject,
                                              //"movetopath", false
                                              ));
        }

    }

    void MoveToBack()
    {
        int routeB = Random.Range(1, 3);
        if (routeB == 1)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPathReversed("Path_1"),
                                                  "time", 10,
                                                   "easetype", iTween.EaseType.linear,
                                                   "looptype", iTween.LoopType.pingPong));
        }
        if (routeB == 2)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPathReversed("Path_2"),
                                                  "time", 10,
                                                   "easetype", iTween.EaseType.linear,
                                                   "looptype", iTween.LoopType.pingPong));
        }
        if (routeB == 3)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPathReversed("Path_3"),
                                                  "time", 10,
                                                   "easetype", iTween.EaseType.linear,
                                                   "looptype", iTween.LoopType.pingPong));
        }
    }

}

