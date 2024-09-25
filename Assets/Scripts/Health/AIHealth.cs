using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// This script will manage all things related to the health of the AI.
public class AIHealth : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField] private Score score;


    [Header("Scriptable Object")]

    [SerializeField] private AIStatistics aiStatistics;


    public float aiHealth;


    /// Start is called before the first frame update
    void Start()
    {
        aiHealth = aiStatistics.health;
    }


    /// Update is called every frame
    private void Update()
    {
        
    }


    /// TakeDamage is called everytime the AI takes damage.
    public void TakeDamage(float damage)
    {
        aiHealth -= damage;


        // Checks if the "AIHealth" float is under 1.
        if (aiHealth < 1)
        {
            // Executes void DIe.
            Die();
        }
    }

    /// Die is called everytime an AI dies.
    private void Die()
    {
        // Destroyes the gameobejct this script is on.
        Destroy(gameObject);
        // Adds the amount of points the enemy gave to the player to the players point tally.
        score.AddPoints(aiStatistics.points);


    }
}
