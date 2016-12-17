using UnityEngine;
using System.Collections;

public class ObserverAnimator : MonoBehaviour {

    Animator observerAnimator;
    public Transform observer;


    void Start () {
        //Player = GetComponentInParent<Transform>();
        observerAnimator = GetComponent<Animator>();
    }

    void Update () {
//        Debug.Log("Angle test : " + observer.eulerAngles.z);
        observerAnimator.SetFloat("ViewAngle", observer.eulerAngles.z);
    }
}
