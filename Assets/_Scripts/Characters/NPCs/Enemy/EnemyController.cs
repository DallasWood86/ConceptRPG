using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCAI))]
public class EnemyController : MonoBehaviour
{
    public LayerMask targetMask;

    private NPCMovement movement;
    private EnemyCombat combat;
    private NPCAI ai;

    private Transform target;
    private Vector3 spawnPoint;

    private float wanderDistance = 50f;

    enum EnemyStates
    {
        Wandering,
        Engaging,
        Attacking
    }

    private EnemyStates curState;


    private void Start()
    {
        movement = GetComponent<NPCMovement>();
        combat = GetComponent<EnemyCombat>();
        ai = GetComponent<NPCAI>();

        // spawnPoint = transform.position;
        spawnPoint = transform.localPosition;

        curState = EnemyStates.Wandering;
    }

    private void Update()
    {
        switch (curState)
        {
            case EnemyStates.Wandering:
                Wander();
                break;
            case EnemyStates.Engaging:
                Engage();
                break;
            case EnemyStates.Attacking:
                Attack();
                break;
        }
    }

    private void Wander()
    {
        if (ai.CanSeeTarget(targetMask))
        {
            target = ai.GetCurrentTarget();
            curState = EnemyStates.Engaging;
        }
        else
        {
            movement.MoveTowards(spawnPoint);
        }
    }

    private void Engage()
    {
        //  if we have travelled too far from the spawn point OR if the target is out of sight go back to the spawn point and wander
        if ((Vector3.Distance(transform.position, spawnPoint) > wanderDistance) || (Vector3.Distance(transform.position, target.position) > 10f))
        {
            curState = EnemyStates.Wandering;
            target = null;
        }
        //  if the target is still in sight but not in combat range, move towards
        else if (!combat.PlayerInRange())
        {
            movement.MoveTowards(target.position);
        }
        //  if the target is within combat range attack
        else
        {
            curState = EnemyStates.Attacking;
        }
    }

    private void Attack()
    {
        if (combat.PlayerInRange())
        {

            if (!combat.isAttacking)
            {
                movement.RotateTowards(target.position);
                combat.StartAttack();
            }
        }
        else
        {
            curState = EnemyStates.Engaging;
        }
    }
}
