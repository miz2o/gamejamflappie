using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float gameTimer;
    public float points, highscore;

    public TextMeshProUGUI timer;

    public bool timerOn, gameOver;
    private void Update()
    {
        timer.text = gameTimer.ToString("F2");
        if (timerOn)
        {
            gameTimer -= Time.deltaTime;
        }

        if (gameTimer < 0)
        {
            timerOn = false;
            gameOver = true;
            //do things
        }
    }
    public void AddPoints(float pointsEarned)
    {
        points = +pointsEarned;
    }

    public void SetHighscore()
    {
        //set new highscore whoooo
    }
}
