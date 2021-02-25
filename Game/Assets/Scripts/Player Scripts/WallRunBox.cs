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
            //GetNormals();
            player.SetLastWalls(other.gameObject);
            player.getNextWall = false;
            player.rigRotation = transform.rotation;
            player.wallRunVelocity = player.GetVelocity();

        }

        if (other.CompareTag("WallRun") && !player.isGrounded && ((player.lastWall1 != player.lastWall2)))
            //|| ((player.lastWall1 == player.lastWall2) && (Vector3.Angle(player.lastNormalVector1, player.lastNormalVector2) > 90))))
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

    /*
    public void GetNormals()
    {
        
        LayerMask playerLayer = 1 << 8;
        playerLayer = ~playerLayer;
        Ray ray;
        RaycastHit hit;
        if (isLeftBox)
            ray = new Ray(transform.position, -transform.right);
        else
            ray = new Ray(transform.position, transform.right);
        if(Physics.Raycast(ray,out hit, 2f, playerLayer))
        {
            player.lastNormalVector = hit.normal;
            player.SetLastWalls(hit.transform.gameObject);
            player.getNextWall = false;
            Debug.DrawRay(transform.position, hit.normal, Color.cyan, 5f);

            //player.SetNormals(hit.normal);
            //Debug.Log(Vector3.Angle(player.lastNormalVector1, player.lastNormalVector2));
            //Debug.Log(player.lastWall1 == player.lastWall2);
        }
    }*/
}
