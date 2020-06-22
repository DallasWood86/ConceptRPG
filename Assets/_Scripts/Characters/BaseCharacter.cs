using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected int maxHitpoints = 100;
    protected int curHitpoints;

    protected bool isDead;

    private void Start()
    {
        curHitpoints = maxHitpoints;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " took " + damage + " damage");
        curHitpoints -= damage;

        if (curHitpoints <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
