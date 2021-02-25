using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBaseBox : MonoBehaviour
{
    /*
    public bool stillInWallRunBaseBox;
    public PlayerMovementRigidbody player;

    
    private void OnTriggerStay(Collider other)
    {
        if ((player.lastWall1 == player.lastWall2) && (player.lastNormalVector1 == player.lastNormalVector2))
        {
            stillInWallRunBaseBox = false;
            return;
        }
        if (other.CompareTag("WallRun") && player.canDoInput && !player.isGrounded)
        {
            stillInWallRunBaseBox = true;
        }
        else
            stillInWallRunBaseBox = false;
    }*/

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            player.rigRotation = transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            player.rigRotation = player.transform.rotation;
        }
    }*/
}
