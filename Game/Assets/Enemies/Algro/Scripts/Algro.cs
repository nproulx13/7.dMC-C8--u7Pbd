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
    public GameObject player;
    private float counter = 0;
    public override void setTime(float f)
    {
        localTime = f;
        bool frozen = f == 0;
        if (frozen)
            gameObject.layer = 8;
        else
            gameObject.layer = 9;
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovementRigidbody>().gameObject;
    }
    void Start()
    {
        reload = 0;
    }

    void Update()
    {
        if (occupied && reload>0)
        {
            reload -= Time.deltaTime * localTime;
        }
        if (reload <= 0)
        {
            reload = reloadTime;
            GameObject g = Instantiate(hyper,transform);
            g.GetComponent<Shiftable>().timeZone = GetComponent<Shiftable>().timeZone;
            g.GetComponent<HyperCube>().targ = player;
            g.GetComponent<HyperCube>().parent = gameObject;
        }

        if (localTime == 0) return;
        counter += localTime * Time.deltaTime;
        counter %= 180;
        float f = (localTime * Mathf.Abs(Mathf.Sin(counter)) * .15f) + 1f;
        transform.localScale = new Vector3(f, f, f);
    }

}
