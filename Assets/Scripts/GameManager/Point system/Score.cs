using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float gameTimer;
    public static float  highScore;
    public int points;

    public TextMeshProUGUI timer, pointUi, timeUpPoints;
    public GameObject timeUpScreen;

    public bool timerOn;
    public static bool gameOver;
    private void Start()
    {
        points = 0;
    }

    private void Update()
    {
        timer.text = gameTimer.ToString("F2");
        if (timerOn)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer < 0)
            {
            
                timerOn = false;
                gameOver = true;

                timeUpScreen.SetActive(true);
                

                timeUpPoints.text = ("Your score:" + points);
                //time over screen with points
                //do things
            }
        }

    }
    public void AddPoints(int pointsEarned)
    {
        points = +pointsEarned;

        if (points > highScore)
        {
            SetHighscore();
        }
    }

    public void SetHighscore()
    {
        //set new highscore whoooo
        highScore = points;
        PlayerPrefs.SetFloat("highScore", highScore);
    }
}
