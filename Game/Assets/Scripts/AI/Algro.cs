using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algro : Controller
{
    private float localTime;
    private bool occupied;
    public float reload = 2;
    public GameObject hyper;
    private GameObject targ;
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
        if (occupied)
        {
            reload -= Time.deltaTime * localTime;
        }
        if (reload < 0)
        {
            reload = 2;
            GameObject g = Instantiate(hyper,transform);
            g.GetComponent<Shiftable>().timeZone = GetComponent<Shiftable>().timeZone;
            g.GetComponent<HyperCube>().targ = targ;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!occupied && other.tag == "Player")
        {
            targ = other.gameObject;
            occupied = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (occupied && other.tag == "Player") occupied = false;
        reload = 2;
    }

}
