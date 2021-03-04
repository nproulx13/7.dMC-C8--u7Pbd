using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyJumpBox : MonoBehaviour
{
    public bool canJumpOffEnemy = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<JumpOffEnemyBox>() != null && other.gameObject.layer == 8 && (transform.parent.position.y > other.transform.position.y))
        {
            canJumpOffEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<JumpOffEnemyBox>() != null && other.gameObject.layer == 8)
        {
            canJumpOffEnemy = false;
        }
    }
}
