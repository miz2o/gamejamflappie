using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI", menuName = "AIStatistics")]


public class AIStatistics : MonoBehaviour
{
    [Header("statistics")]

    public float health;
    public float movementSpeed;
    public float rotationSpeed;

    public int points;
}
