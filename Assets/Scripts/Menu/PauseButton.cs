using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour {

    public void ClickPauseButton()
    {
        Debug.Log("asdf");
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("PauseButton >>> ClickPauseButton()");
            LevelManager.Instance.PauseGame();
        }
    }
}
