using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private Node[,] grid;
    [SerializeField] private float nodeRadius;
    [SerializeField] private LayerMask unwalkebleMask;

    private float nodeDiamiter;
    private int gridSizeY;
    private int gridSizeX;


    [SerializeField] private bool displayGridGizmos;

    private void Awake()
    {
        nodeDiamiter = nodeRadius * 2;


        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamiter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamiter);


        CreateGrid();
    }


    public int MaximalSize
    {
        get 
        { 
            return gridSizeX * gridSizeY;
        }
    }


    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldPointBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        

        for (int x = 0; x < gridSizeX; x ++)
        {
            for (int y = 0; y < gridSizeY; y ++)
            {
                Vector3 worldpoint = worldPointBottomLeft + Vector3.right * (x * nodeDiamiter + nodeRadius) + Vector3.forward * (y * nodeDiamiter + nodeRadius);

                bool walkable = ! (Physics.CheckSphere(worldpoint, nodeRadius, unwalkebleMask));

                grid[x, y] = new Node(walkable, worldpoint, x, y);
            }
        }
    }


    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>(); 

        for (int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }


                int checkX = node.gridX + x;
                int checkY = node.gridY + y;


                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }


        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldposition)
    {
        float xPercentage = (worldposition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float yPercentage = (worldposition.z + gridWorldSize.x / 2) / gridWorldSize.y;


        xPercentage = Mathf.Clamp01(xPercentage);
        yPercentage = Mathf.Clamp01(yPercentage);


        int x = Mathf.RoundToInt((gridSizeX - 1) * xPercentage);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPercentage);


        return grid[x, y];
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;


        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));


        if (grid != null && displayGridGizmos == true)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isWalkable == true) ? Color.white:Color.red;


                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiamiter - 0.1f));
            }
        }
    }
}
