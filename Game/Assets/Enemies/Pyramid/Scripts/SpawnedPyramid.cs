using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPyramid : Controller
{
    public GameObject hitParticle;
    public GameObject parent;
    [SerializeField] private PyramidSpawnCollider spawnCollider;
    private Vector3 velocity;
    private Rigidbody rbody;
    private float localTime;
    private float timeTillDestroy = 0;
    private float timeToDestroy = 7f;
    public bool frozen = false;


    public override void setTime(float f)
    {
        localTime = f;
        frozen = f == 0;
        if (frozen)
        {
            spawnCollider.gameObject.layer = 8;
            gameObject.layer = 8;
        }
        else
        {
            spawnCollider.gameObject.layer = 9;
            gameObject.layer = 9;
        }
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        velocity = rbody.velocity;
        setTime(TimeCore.times[GetComponent<Shiftable>().timeZone]);
    }

    private void Update()
    {
        if (!frozen)
        {
            rbody.isKinematic = false;
            rbody.velocity = velocity * localTime;
            timeTillDestroy += Time.deltaTime * localTime;
            if (timeTillDestroy == timeToDestroy)
            {
                if (hitParticle != null) Instantiate(hitParticle);
                Destroy(gameObject);
            }
        }
        else
        {
            rbody.velocity = Vector3.zero;
            rbody.isKinematic = true;
        }
    }
}
