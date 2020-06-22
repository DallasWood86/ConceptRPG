using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;

    void Awake()
    {
        player = GetComponent<Player>();
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //  COMBAT CONTROLS
        if (Input.GetMouseButton(0) && playerMovement.IsGrounded() && !playerCombat.GetWeaponSheathed())
        {
            playerCombat.StartAttack();
        }

        if (Input.GetButtonDown("Sheath"))
        {
            playerCombat.ToggleSheath();
        }

        //  DASH CONTROLS
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerCombat.DashForward();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerCombat.DashBackward();
            }
        }



        //  MOVEMENT CONTROLS
        if (!playerCombat.isAttacking)
        {
            playerMovement.Move();
        }

        if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
        {
            playerMovement.StartJump();
        }

        playerMovement.Rotate();
    }


}
