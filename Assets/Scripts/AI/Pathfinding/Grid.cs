using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private Node[,] grid;
    [SerializeField] private float nodeRadius;
    [SerializeField] private LayerMask unwalkebleMask;

    private float nodeDiamiter;
    private int gridSizeY;
    private int gridSizeX;


    private void Start()
    {
        nodeDiamiter = nodeRadius * 2;


        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamiter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamiter);
    }


    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];


        for (int x = 0; x < gridSizeX; x ++)
        {

        }


        for (int y = 0; y < gridSizeX; y ++)
        {

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
    }
}
