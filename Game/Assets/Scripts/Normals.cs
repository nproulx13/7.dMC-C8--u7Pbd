using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normals : MonoBehaviour
{
    Vector3 normal = Vector3.zero;
    // Start is called before the first frame update
    private void Update()
    {
        Debug.DrawRay(transform.position, normal);
    }

    private void OnCollisionEnter(Collision collision)
    {
        normal = -collision.GetContact(0).normal;
    }
}
