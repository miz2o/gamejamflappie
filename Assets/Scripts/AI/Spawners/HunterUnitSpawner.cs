using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Type")]

    [SerializeField] private GameObject hunterUnit;
    [SerializeField] private GameObject[] civilianUnits;


    [Header("Unit Amounts")]

    [SerializeField] private int minimalUnitsToSpawn;
    [SerializeField] private int maximalUnitsToSpawn;


    [Header("Spawn Radius")]

    [SerializeField] private float spawnRange;


    [Header("Layermask")]

    [SerializeField] private LayerMask ground;


    private void Start()
    {

    }


    private void HunterUnitSpawner()
    {
        int unitsToSpawn = Random.Range(minimalUnitsToSpawn, maximalUnitsToSpawn + 1);


    }
}
