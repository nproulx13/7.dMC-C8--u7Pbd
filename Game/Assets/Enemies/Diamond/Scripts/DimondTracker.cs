using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimondTracker : MonoBehaviour
{
    [SerializeField] private Dimond dimond;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) dimond.isTracking = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) dimond.isTracking = false;

    }
}
