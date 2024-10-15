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
}
