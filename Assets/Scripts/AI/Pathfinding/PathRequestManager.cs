using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour
{
    Pathfinding pathFinding;

    private static PathRequestManager instance;
    
    
    private Queue<PathRequest> pathRequests = new Queue<PathRequest>();


    private PathRequest currentPathRequest;


    private bool isProccesingPath;


    private void Awake()
    {
        instance = this;


        pathFinding = GetComponent<Pathfinding>();
    }


    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callBack);

        
        instance.pathRequests.Enqueue(newRequest);


        instance.TryProccesNext();
    }


    private void TryProccesNext()
    {
        if (!isProccesingPath && pathRequests.Count > 0)
        {
            currentPathRequest= pathRequests.Dequeue();


            isProccesingPath= true;


            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }


    public void FinishedProcessingPath(Vector3[] path, bool succes)
    {
        currentPathRequest.callBack(path,succes);


        isProccesingPath = false;


        TryProccesNext();
    }


    public struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;

        public Action<Vector3[], bool> callBack;


        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callBack)
        {
            pathStart = _start;
            pathEnd= _end;

            callBack= _callBack;
        }
    }
}
