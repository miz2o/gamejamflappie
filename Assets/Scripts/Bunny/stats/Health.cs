using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Score score;
    public AIStatistics aiStatistics;

    public float health, maxHealth;
    public TextMeshProUGUI pointsEarned;
    public GameObject deathPanel;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.tag == "Player")
        {
            deathPanel.SetActive(true);
            pointsEarned.text = ("Your score:" + score.points);
        }


        else if (gameObject.tag == "Enemy")
        {
            score.AddPoints(aiStatistics.points);
        }
        //gameOver screen with point overview
        //stuff when dead
    }
}
