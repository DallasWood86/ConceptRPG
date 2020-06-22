using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = PlayerManager.Instance.player.transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + target.forward);
    }
}
