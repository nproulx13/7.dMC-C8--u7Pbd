using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgroSight : MonoBehaviour
{
    public Algro a;
    private void OnTriggerEnter(Collider other)
    {
        if (!a.occupied && other.tag == "Player")
        {
            a.player = other.gameObject;
            a.occupied = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (a.occupied && other.tag == "Player") a.occupied = false;
        a.reload = a.reloadTime/2;
    }
}
