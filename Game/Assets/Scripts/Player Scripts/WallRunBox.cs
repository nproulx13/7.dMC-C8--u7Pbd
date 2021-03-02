using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBox : MonoBehaviour
{
    private PlayerMovementRigidbody player;
    public bool isRightBox;
    public bool isLeftBox;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementRigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.isGrounded)
        {
            player.getNextWall = true;
            return;
        }

        if (player.getNextWall && other.CompareTag("WallRun"))
        {
            player.SetLastWalls(other.gameObject);
            player.getNextWall = false;
            player.rigRotation = transform.rotation;
            player.wallRunVelocity = player.GetVelocity();

        }

        if (other.CompareTag("WallRun") && !player.isGrounded && ((player.lastWall1 != player.lastWall2)))
        {
            if (isRightBox)
            {
                player.isWallRunning = true;
                player.isWallRunningRight = true;
                player.isWallRunningLeft = false;
            }

            else if (isLeftBox)
            {
                player.isWallRunning = true;
                player.isWallRunningLeft = true;
                player.isWallRunningRight = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            player.isWallRunning = false;
            player.isWallRunningLeft = false;
            player.isWallRunningRight = false;
            player.rigRotation = player.transform.rotation;
            player.getNextWall = true;
        }
    }
}
