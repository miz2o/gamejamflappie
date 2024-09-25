using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    [SerializeField] private bool isWakable;
    [SerializeField] private Vector3 worldPosition;


    private Node(bool _isWalkable, Vector3 _worldPosition) 
    {
        isWakable= _isWalkable;

        worldPosition= _worldPosition;



    } 
}
