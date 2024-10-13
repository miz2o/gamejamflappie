/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float speed = 5f;
    private Vector3[] path;
    private int targetIndex;


    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }


    private void OnPathFound(Vector3[] newPath, bool pathSuccessfull)
    {
        print("outside if");
        //if(pathSuccessfull == true)
       // {
            path = newPath;
            print("in if");
            targetIndex = 0;    

            StopCoroutine(FollowPath());


            StartCoroutine(FollowPath());
       // }
    }



    public IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        print("inside ie numerator");

        while (true)
        {

            print("inside while");
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                print("inside while if");

                if (targetIndex >= path.Length)
                {
                    print("inside while if if");
                    yield break;
                }


                currentWaypoint= path[targetIndex];
            }


            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);


            yield return null;
        }
    }
}
*/

using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    public Transform target;
    float speed = 20;
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
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