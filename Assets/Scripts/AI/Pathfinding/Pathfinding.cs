using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using System;

public class Pathfinding : MonoBehaviour
{
    private PathRequestManager pathRequestManager;
    private Grid grid;


    void Awake()
    {
        pathRequestManager= GetComponent<PathRequestManager>();


        grid = GetComponent<Grid>();
    }


    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        StartCoroutine(FindPath(startPosition, targetPosition));
    }


    public IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3[]waypoints= new Vector3[0];

        bool pathSuccess = false;


        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);


        if (startNode.isWalkable == true && targetNode.isWalkable == true)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaximalSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.AddItem(startNode);


            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirstItem();


                closedSet.Add(currentNode);


                if (currentNode == targetNode)
                {
                    pathSuccess = true;


                    break;
                }


                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    

                    int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;


                        if (!openSet.Contains(neighbour))
                        {
                            openSet.AddItem(neighbour);
                        }

                        else
                        {
                            openSet.UpdateItems(neighbour);
                        }
                    }
                }
            }
        }

                
        yield return null;


        if (pathSuccess == true)
        {
            waypoints = RetracePath(startNode, targetNode);
        }


        pathRequestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    private Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
                
        Node currentNode = endNode;


        while (currentNode != startNode)
        {
            path.Add(currentNode);


            currentNode = currentNode.parent;
        }


        Vector3[] waypoints = simplefyPath(path);


        Array.Reverse(waypoints);


        return waypoints;

    }


    private Vector3[] simplefyPath(List<Node> path)
    {
        List<Vector3> waypoints= new List<Vector3>();

        Vector2 oldDirection = Vector2.zero;


        for (int i =1; i < path.Count; i++)
        {
            Vector2 newDirection = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);


            if (newDirection != oldDirection)
            {
                waypoints.Add(path[i].worldPosition);
            }


            oldDirection = newDirection;
        }


        return waypoints.ToArray();
    }


    public int GetDistance(Node nodeA, Node nodeB)
    {
        int xAxisDistance = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int yAxisDistance = Mathf.Abs(nodeA.gridY - nodeB.gridY);


        if (xAxisDistance > yAxisDistance)
        {
            return 14 * yAxisDistance + 10 * (xAxisDistance - yAxisDistance);
        }

        else
        {
            return 14 * xAxisDistance + 10 * (yAxisDistance - xAxisDistance);
        }

    }
}