using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimondProjectile : Controller
{
    [SerializeField] private GameObject hitParticle;
    public GameObject parent;
    private Vector3 velocity;
    private Rigidbody rbody;
    private float timeTillDestroy = 0;
    private float timeToDestroy = 3f;
    private bool frozen = false;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        velocity = rbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!frozen)
        {
            Debug.Log("<color=red>Dead</color>");
        }
        else if(collision.gameObject!=parent && !frozen)
        {
            Debug.Log("<color=yellow>Destroyed</color>");
            if(hitParticle!=null) Instantiate(hitParticle);
            Destroy(gameObject);
        }

    }

    public override void setTime(float f)
    {
        frozen = f == 0;
    }

    private void Update()
    {
        if (!frozen)
        {
            transform.Rotate(0, 0, 200 * Time.deltaTime);
            rbody.isKinematic = false;
            rbody.velocity = velocity;
            timeTillDestroy += Time.deltaTime;
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
