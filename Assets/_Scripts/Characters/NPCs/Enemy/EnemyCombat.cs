using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : BaseCombat
{
    private int damage = 5;
    private float attackSpeed = 3f;


    protected override void ApplyDamage(BaseCharacter target)
    {
        target.TakeDamage(damage);
    }

    public bool PlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        if (colliders.Length > 0)
        {
            return true;
        }

        return false;
    }

    public override void StartAttack()
    {
        base.StartAttack();
        animator.SetTrigger("Attack");
        StartCoroutine(AttackTimer());
    }

    private IEnumerator AttackTimer()
    {
        Attack();
        yield return new WaitForSeconds(attackSpeed);
        FinishAttack();
    }


}
