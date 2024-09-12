using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyStats : MonoBehaviour
{
    public float health, maxHealth;


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
        Debug.Log("You're dead... What a skill issue");
        //stuff when dead
    }
}
