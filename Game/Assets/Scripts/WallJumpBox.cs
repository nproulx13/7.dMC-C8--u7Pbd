using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpBox : MonoBehaviour
{
    public bool canWallJump = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("WallRun"))
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
