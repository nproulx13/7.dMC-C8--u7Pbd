using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovementRigidbody player;
    public LayerMask layer;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = true;
        }

        if (other.gameObject.CompareTag("WallRun"))
        {
            player.isGrounded = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
        }
    }
}
