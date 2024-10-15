using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;  // The target destination
    public float speed = 5;   // Speed of the unit's movement
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        UpdatePath(); // Initiate the first path calculation
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if(targetIndex < path.Length) 
        {
            Vector3 currentWaypoint = path[targetIndex];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break; // Reached the end of the path
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null; // Wait until the next frame
            }
        }
    }

    void Update()
    {
        // Continuously check if the target's position has changed
        if (Vector3.Distance(transform.position, target.position) > 0f)
        {
            UpdatePath(); // Update the path if the target has moved
        }
    }

    public void UpdatePath()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
