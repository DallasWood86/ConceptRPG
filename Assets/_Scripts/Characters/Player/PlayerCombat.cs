using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombat
{
    //  eventually this will come from the weapon/player itself
    private int damage = 5;

    private int combo, tempCombo;
    private int maxCombo = 3;


    private bool weaponSheathed = true;

    public GameObject weaponSocket;
    private WeaponScript weapon;

    private void Start()
    {
        //  start with weapon sheathed
        weaponSocket.SetActive(!weaponSheathed);
        weapon = GetComponentInChildren<WeaponScript>();
    }
    protected override void ApplyDamage(BaseCharacter target)
    {
        target.TakeDamage(damage);
    }

    public override void StartAttack()
    {
        if (!isAttacking)
        {
            base.StartAttack();

            //  reset my combo
            combo = 0;
            tempCombo = 0;

            //  tell the animator to start the attack
            animator.SetTrigger("Attack");
        }
        else
        {
            //  if we are already attacking, add 1 to the combo
            tempCombo++;
        }

    }

    //  finish attack is called a couple frames before the animation is over
    public override void FinishAttack()
    {
        //  if we have a combo stored, apply it
        if (tempCombo > combo)
        {
            //  if we are coming from our final combo, we want to move into the second combo
            if (combo == maxCombo)
            {
                combo = 1;
            }
            else
            {
                combo++;
            }
        }
        else
        {
            //  if we do not have a stored combo, finish our attack
            base.FinishAttack();
            combo = 0;
        }
        //  tell our animator to update the combo to move to the next combo state
        animator.SetInteger("Combo", combo);
        //  reset our temp combo to our current combo
        tempCombo = combo;
    }

    public void ToggleSheath()
    {
        weaponSheathed = !weaponSheathed;
        weaponSocket.SetActive(!weaponSheathed);
    }

    public bool GetWeaponSheathed()
    {
        return weaponSheathed;
    }

    public void DashForward()
    {
        animator.SetTrigger("DashForward");
    }

    public void DashBackward()
    {
        animator.SetTrigger("DashBackward");
    }
}
