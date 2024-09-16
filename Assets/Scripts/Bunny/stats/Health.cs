using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
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
        if (gameObject.tag == "Player")
        {
            deathPanel.SetActive(true);
            pointsEarned.text = ("Your score:" + Score.points);
        }


        else if (gameObject.tag == "Enemy")
        {

        }
        //gameOver screen with point overview
        //stuff when dead
    }
}
