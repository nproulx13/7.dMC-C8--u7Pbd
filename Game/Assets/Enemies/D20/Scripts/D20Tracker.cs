using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Tracker : MonoBehaviour
{
    [SerializeField] private D20 d20;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) d20.isTracking = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) d20.isTracking = false;

    }
}
