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
        if(player.isGrounded)
        {
            return;
        }
        else if (other.CompareTag("WallRun") && (other.gameObject!=player.lastWall || (other.gameObject == player.lastWall && (player.lastNormalVector1 != player.lastNormalVector2))))
        {
            if (isRightBox)
            {
                player.isWallRunning = true;
                player.isWallRunningRight = true;
                player.isWallRunningLeft = false;
                //Debug.DrawRay(transform.position, (other.transform.position - transform.position), Color.red, 10f);
                //player.wallRunDirection = -Vector3.Cross(other., Vector3.up);
            }

            else if (isLeftBox)
            {
                player.isWallRunning = true;
                player.isWallRunningLeft = true;
                player.isWallRunningRight = false;
            }
            player.lastWall = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            player.isWallRunning = false;
            player.isWallRunningLeft = false;
            player.isWallRunningRight = false;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (player.isGrounded)
        {
            return;
        }

        if (other.CompareTag("WallRun"))
        {
            Ray ray = isLeftBox ? new Ray(transform.position,-transform.right) : new Ray(transform.position, -transform.right);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.point;
                Vector3 rayBackToBox = point - transform.position;
                Debug.Log(rayBackToBox);
                if (player.oneOrTwoSwitchForNormalVectors == 1)
                {
                    player.lastNormalVector1 = rayBackToBox;
                    player.oneOrTwoSwitchForNormalVectors = 2;
                }
                else
                {
                    player.lastNormalVector1 = rayBackToBox;
                    player.oneOrTwoSwitchForNormalVectors = 1;
                }
            }
           
        }
    }*/
}
