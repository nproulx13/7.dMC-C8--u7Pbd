using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCube : Controller
{
    public GameObject targ;
    private float burst = 2;
    private float localTime;
    private Vector3 last;
    private bool go;

    public override void setTime(float f)
    {
        localTime = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (burst < -1)
        {
            go = false;
            burst = 2;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (burst < 0 && !go)
        {
            burst -= Time.deltaTime * localTime;
            last = targ.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.Normalize(transform.position - last) * -25;
            go = true;
        }
        else if (go)
            burst -= Time.deltaTime * localTime;
        else
        {
            burst -= Time.deltaTime * localTime;
        }
    }
}
