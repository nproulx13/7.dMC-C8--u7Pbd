using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpBox : MonoBehaviour
{
    public PlayerMovementRigidbody player;
    public bool canWallJump = false;
    
    private void OnTriggerStay(Collider other)
    {
        /*
        if ((player.lastWall1 == player.lastWall2) && (player.lastNormalVector1 == player.lastNormalVector2))
        {
            canWallJump = false;
            return;
        }*/
        if (player.isWallRunning)
        {
            canWallJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            canWallJump = false;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallRun") && !player.isGrounded)
            player.SetLastWalls(other.gameObject);
        /*
        if ((other.gameObject == player.lastWall) && (player.lastNormalVector1 == player.lastNormalVector2))
        {
            canWallJump = false;
            return;
        }
        if (other.CompareTag("WallRun") && !player.isGrounded)
        {
            canWallJump = true;
        }
    }*/
}
