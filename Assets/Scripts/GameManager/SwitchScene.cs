using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public GameObject highscoreMenu;

    bool highscoreMenuOn;
    public void ClickButton(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void TurnMenuO()
    {
        if (highscoreMenuOn)
        {
            highscoreMenu.SetActive(false);
            highscoreMenuOn = false;
        }
        else
        {
            highscoreMenuOn = true;
            highscoreMenu.SetActive(true);
        }
    }
}
