using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidSpawnCollider : MonoBehaviour
{
    [SerializeField] private SpawnedPyramid spawnedPyramid;
    Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rbody.velocity = spawnedPyramid.GetComponent<Rigidbody>().velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !spawnedPyramid.frozen)
        {
            //Debug.Log("<color=red>Dead</color>");
            Destroy(transform.parent.gameObject);
        }
        else if (collision.gameObject != spawnedPyramid.parent && spawnedPyramid.frozen)
        {
            //Debug.Log("<color=yellow>Destroyed</color>");
            if (spawnedPyramid.hitParticle!= null) Instantiate(spawnedPyramid.hitParticle);
            Destroy(gameObject);
        }

    }
}
