using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianUnit : MonoBehaviour
{

    private LineOfSight lineOfSight;


    [Header("Scriptable Objects")]

    [SerializeField] private AIStatistics aiStatistics;


    [SerializeField] private Transform targetObject;


    private float thisSpeed;

    private void Awake()
    {
        lineOfSight = GetComponent<LineOfSight>();


        thisSpeed = aiStatistics.movementSpeed;
    }


    private void Update()
    {
        if (lineOfSight.playerIsViseble == true)
        {
            // Calculate the direction away from the target object
            Vector3 directionAway = (transform.position - targetObject.position).normalized;

            // Move the object in the opposite direction from the target
            transform.position += directionAway * thisSpeed * Time.deltaTime;
        }
    }
}
