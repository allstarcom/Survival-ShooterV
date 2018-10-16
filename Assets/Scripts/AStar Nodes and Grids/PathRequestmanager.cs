using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestmanager : MonoBehaviour {

    static PathRequestmanager instance;
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    PathFinding pathFinding;
    bool isProcessing;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
       
    }

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newPathRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newPathRequest);
        instance.TryProcessingNext();


    }

    void TryProcessingNext()
    {
        if(!isProcessing && pathRequestQueue.Count>0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessing = false;
        TryProcessingNext();
    }


    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;


        public PathRequest(Vector3 _Start, Vector3 _End, Action<Vector3[], bool> _callback)
        {
            pathStart = _Start;
            pathEnd = _End;
            callback = _callback;
        }



    }



}
