using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public GameObject highscoreMenu;

    public void ClickButton(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void TurnMenuO()
    {
        if (highscoreMenu)
        {
            highscoreMenu.SetActive(false);
        }
        else
        {
            highscoreMenu.SetActive(true);
        }
    }
}
