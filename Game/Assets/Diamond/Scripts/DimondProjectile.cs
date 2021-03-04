using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimondProjectile : Controller
{
    [SerializeField] private GameObject hitParticle;
    //[SerializeField] private Shiftable shiftable;
    public GameObject parent;
    private Vector3 velocity;
    private Rigidbody rbody;
    private float localTime;
    private float timeTillDestroy = 0;
    private float timeToDestroy = 3f;
    private bool frozen = false;
    private void Awake()
    {
        //shiftable = GetComponent<Shiftable>();
    }
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        velocity = rbody.velocity;
        setTime(TimeCore.times[GetComponent<Shiftable>().timeZone]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!frozen)
        {
            Debug.Log("<color=red>Dead</color>");
        }
        else if(collision.gameObject!=parent && !frozen)
        {
            //Debug.Log("<color=yellow>Destroyed</color>");
            if(hitParticle!=null) Instantiate(hitParticle);
            Destroy(gameObject);
        }

    }

    public override void setTime(float f)
    {
        localTime = f;
        frozen = f == 0;
        if(frozen)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
    }

    private void Update()
    {
        if (!frozen)
        {
            transform.Rotate(0, 0, 200 * Time.deltaTime * localTime);
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
