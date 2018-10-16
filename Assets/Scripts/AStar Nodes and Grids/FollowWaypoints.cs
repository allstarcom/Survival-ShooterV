using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour {

    public Transform target;
    public float speed;
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        PathRequestmanager.RequestPath(transform.position, target.position, OnPathDiscover);
    }

    public void StartPathing(Vector3 target)
    {
        PathRequestmanager.RequestPath(transform.position, target, OnPathDiscover);
    }

    void OnPathDiscover(Vector3[] newPath, bool success)
    {
        if(success)
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

        while(true)
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
            transform.position = Vector3.MoveTowards(transform.position,currentWaypoint, speed* Time.deltaTime);
            yield return null;
        }
    }


    public void OnDrawGizmos()
    {
        if(path!=null)
        {
            for(int i= targetIndex; i<path.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(path[i], Vector3.one);
                if(i==targetIndex)
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
