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
    private float timeToDestroy = 5f;
    public bool frozen = false;
    float counter = 0;


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
            if (timeTillDestroy >= timeToDestroy)
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

        transform.Rotate(Vector3.up * 200 * localTime * Time.deltaTime);
        /*
        if (localTime == 0) return;       
        counter += localTime * Time.deltaTime;
        counter %= 180;
        float f = (localTime * Mathf.Abs(Mathf.Sin(counter)) * .3f) + 1f;
        transform.localScale = new Vector3(f, f, f);*/
    }
}
