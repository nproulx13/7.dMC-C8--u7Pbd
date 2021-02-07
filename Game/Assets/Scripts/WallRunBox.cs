using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBox : MonoBehaviour
{
    private Movement player;
    public bool isRightBox;
    public bool isLeftBox;

    private void Start()
    {
        player = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallRun");
        {
            if (isRightBox)
            {
                player.isWallRunning = true;
                player.isWallRunningRight = true;
                player.isWallRunningLeft = false;
                Debug.DrawRay(transform.position, (other.transform.position - transform.position), Color.red, 10f);
                //player.wallRunDirection = -Vector3.Cross(other., Vector3.up);
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
        if (other.tag == "WallRun")
        {
            player.isWallRunning = false;
            player.isWallRunningLeft = false;
            player.isWallRunningRight = false;
        }
    }
}
