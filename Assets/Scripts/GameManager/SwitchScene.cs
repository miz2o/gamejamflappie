using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void ClickButton(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void MenuDisable(GameObject menuToDisable)
    {
        menuToDisable.SetActive(false);
    }

    public void MenuEnable(GameObject menuToEnable)
    {
        menuToEnable.SetActive(true);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
