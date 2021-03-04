using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algro : Controller
{
    private float localTime;
    public bool occupied;
    public float reload = 0;
    public float reloadTime = 4f;
    public GameObject hyper;
    public GameObject targ;
    public override void setTime(float f)
    {
        localTime = f;
        bool frozen = f == 0;
        if (frozen)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
    }

    // Start is called before the first frame update
    void Start()
    {
        reload = 0;
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
            reload = reloadTime;
            GameObject g = Instantiate(hyper,transform);
            g.GetComponent<Shiftable>().timeZone = GetComponent<Shiftable>().timeZone;
            g.GetComponent<HyperCube>().targ = targ;
            g.GetComponent<HyperCube>().parent = gameObject;
        }
    }

}
