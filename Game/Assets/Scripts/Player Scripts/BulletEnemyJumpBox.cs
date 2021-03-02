using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyJumpBox : MonoBehaviour
{
    public bool canJumpOffEnemy = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<JumpOffEnemyBox>() != null)
        {
            canJumpOffEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            canJumpOffEnemy = false;
        }
    }
}
