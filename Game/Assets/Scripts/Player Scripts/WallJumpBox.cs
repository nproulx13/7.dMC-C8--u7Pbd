using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpBox : MonoBehaviour
{
    public PlayerMovementRigidbody player;
    public bool canWallJump = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallRun") && (other.gameObject != player.lastWall || (other.gameObject == player.lastWall && (player.lastNormalVector1 != player.lastNormalVector2))))
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
}
