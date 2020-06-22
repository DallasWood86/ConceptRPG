using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    private Collider coll;
    private Healthbar healthbar;
    private EnemyController controller;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        healthbar = GetComponentInChildren<Healthbar>();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        healthbar.SetHealth(curHitpoints, maxHitpoints);
    }

    protected override void Die()
    {
        Destroy(gameObject);
        // Destroy(controller);
        // Destroy(coll);
        // Destroy(healthbar.gameObject);
    }


}
