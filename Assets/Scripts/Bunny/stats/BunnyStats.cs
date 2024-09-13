using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BunnyStats : MonoBehaviour
{
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
        deathPanel.SetActive(true);
        pointsEarned.text = ("Your score:" + Score.points);
        //gameOver screen with point overview
        //stuff when dead
    }
}
