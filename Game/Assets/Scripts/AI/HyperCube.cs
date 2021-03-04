using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCube : Controller
{
    public GameObject targ;
    public float burst = 2;
    public float speed = 25;
    public GameObject parent;
    private float localTime;
    private Vector3 last;
    private Vector3 offset;
    private float resetRotation;
    private bool go = false;
    private float current = 1;
    private float slowDown = 0.25f;
    private Rigidbody rbody;

    public override void setTime(float f)
    {
        localTime = f;
        if (f == 0)
            resetRotation = 5;
        bool frozen = f == 0;
        if (frozen)
        {
            rbody.isKinematic = true;
            gameObject.layer = 8;
        }
        else
        {
            rbody.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        localTime = TimeCore.times[GetComponent<Shiftable>().timeZone];
        setTime(localTime);
        StartCoroutine(GetTarget());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(localTime != 0)
        {
            //transform.LookAt(targ.transform);
            Vector3 whereToLook = targ.transform.position - transform.position;
            resetRotation += 2 * Time.deltaTime * localTime;
            if (resetRotation >= 6) resetRotation += 50 * Time.deltaTime * localTime;
            resetRotation = Mathf.Clamp(resetRotation, 5, 100);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(whereToLook, Vector3.up), resetRotation * Time.deltaTime * localTime);
        }*/
        //Use for pyramid
        /*
        if (burst < -1)
        {
            go = false;
            burst = 2;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*/
        /*
        if (burst < 0 && !go)
        {
            last = targ.transform.position + Vector3.up + offset;
            burst -= Time.deltaTime * localTime;
            go = true;
        }
        if (go)
        {
            burst -= Time.deltaTime * localTime;
            GetComponent<Rigidbody>().velocity = (Vector3.Normalize(last - transform.position)) * speed * localTime;
        }

        else
        {
            last = targ.transform.position + Vector3.up + offset;
            GetComponent<Rigidbody>().velocity = (Vector3.Normalize(last - transform.position)) * speed * localTime/5;
            burst -= Time.deltaTime * localTime;
        }*/

        rbody.velocity = rbody.velocity * localTime;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && localTime > 0)
        {
            //Debug.Log("<color=red>Dead</color>");
        }
        else if (collision.gameObject != parent && localTime > 0 && collision.gameObject.layer != 9)
        {
            //Debug.Log("<color=yellow>Destroyed</color>");
            Destroy(gameObject);
        }
    }

    private IEnumerator GetTarget()
    {
        speed = 15;
        last = targ.transform.position + Vector3.up + offset;
        rbody.velocity = ((Vector3.Normalize(last - transform.position)) * speed) * localTime;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));


        rbody.velocity = ((Vector3.Normalize(last - transform.position)) * speed) * localTime;
        speed = 5;
        last = targ.transform.position + Vector3.up + offset;
        yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));

        StartCoroutine(GetTarget());
    }
}
