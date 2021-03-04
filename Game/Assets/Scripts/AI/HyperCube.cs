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

    public override void setTime(float f)
    {
        localTime = f;
        if (f == 0)
            resetRotation = 5;
        bool frozen = f == 0;
        if (frozen)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
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
        {
            //transform.LookAt(player.transform);
            Vector3 whereToLook = targ.transform.position - transform.position;
            resetRotation += 2 * Time.deltaTime * localTime;
            if (resetRotation >= 6) resetRotation += 50 * Time.deltaTime * localTime;
            resetRotation = Mathf.Clamp(resetRotation, 5, 100);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(whereToLook, Vector3.up), resetRotation * Time.deltaTime * localTime);
        }
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
            go = true;
        }
        else if (go)
        {
            burst -= Time.deltaTime * localTime;
            GetComponent<Rigidbody>().velocity = (Vector3.Normalize(last - transform.position)) * speed * localTime;
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
        else if (collision.gameObject != parent && localTime > 0 && collision.gameObject.layer == 8)
        {
            Debug.Log("<color=yellow>Destroyed</color>");
            Destroy(gameObject);
        }
    }
}
