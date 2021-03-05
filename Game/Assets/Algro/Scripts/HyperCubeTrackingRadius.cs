using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCubeTrackingRadius : MonoBehaviour
{
    [SerializeField] private HyperCube hyperCube;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) hyperCube.isTracking = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) hyperCube.isTracking = false;

    }

}
