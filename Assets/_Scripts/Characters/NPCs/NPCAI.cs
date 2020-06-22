using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{

    float lookRadius = 15f;

    public Transform target;

    public bool CanSeeTarget(LayerMask targetMask)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lookRadius, targetMask);
        if (colliders.Length > 0)
        {
            //  if there were any collisions, return the first one
            target = colliders[0].transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform GetCurrentTarget()
    {
        return target;
    }
}
