using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyUpdatePath: MonoBehaviour
    {

       FollowWaypoints unit;
        bool hasTarget;
        float refreshRate = 0.9f;
        Transform target;

        void Awake()
        {
            unit = GetComponent<FollowWaypoints>();
            if (GameObject.FindGameObjectWithTag("Player").transform)
            {
                hasTarget = true;
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        void Start()
        {
            StartCoroutine(UpdatePath());
        }

        IEnumerator UpdatePath()
        {
            while (hasTarget)
            {
                unit.StartPathing(target.position);
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }



