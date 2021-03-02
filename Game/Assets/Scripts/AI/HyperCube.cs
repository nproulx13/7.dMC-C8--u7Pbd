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
    private bool go = false;

    public override void setTime(float f)
    {
        localTime = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        localTime = TimeCore.times[GetComponent<Shiftable>().timeZone];
    }

    // Update is called once per frame
    void Update()
    {
        if(localTime != 0)
        transform.LookAt(targ.transform);
        //Use for pyramid
        if (burst < -1)
        {
            go = false;
            burst = 2;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (burst < 0 && !go)
        {
            last = targ.transform.position + Vector3.up + offset;
            burst -= Time.deltaTime * localTime;
            GetComponent<Rigidbody>().velocity = (Vector3.Normalize(last - transform.position)) * speed * localTime;
            go = true;
        }
        else if (go)
        {
            burst -= Time.deltaTime * localTime;
        }
        else
        {
            last = targ.transform.position + Vector3.up + offset;
            GetComponent<Rigidbody>().velocity = (Vector3.Normalize(last - transform.position)) * speed * localTime/5;
            burst -= Time.deltaTime * localTime;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && localTime > 0)
        {
            Debug.Log("<color=red>Dead</color>");
        }
        else if (collision.gameObject != parent && localTime > 0)
        {
            Debug.Log("<color=yellow>Destroyed</color>");
            Destroy(gameObject);
        }
    }
}
