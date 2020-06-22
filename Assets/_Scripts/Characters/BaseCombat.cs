using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseCombat : MonoBehaviour
{
    protected Animator animator;
    public LayerMask targetMask;

    public bool isAttacking;
    public float attackRange = 3f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //  this is called from the controller to begin the attack
    public virtual void StartAttack()
    {
        isAttacking = true;
    }

    public virtual void FinishAttack()
    {
        isAttacking = false;
    }

    public void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);

        foreach (Collider collider in colliders)
        {

            BaseCharacter target;
            if (target = collider.gameObject.GetComponent<BaseCharacter>())
            {
                ApplyDamage(target);
            }
        }
    }

    protected virtual void ApplyDamage(BaseCharacter target) { }

}
