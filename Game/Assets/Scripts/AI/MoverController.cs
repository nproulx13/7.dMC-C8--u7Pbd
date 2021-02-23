using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : Controller
{
    private float localTime = 0;
    private float counter = 0;
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
        counter += localTime * Time.deltaTime;
        counter %= 180;
        GetComponent<Rigidbody>().velocity = new Vector3(localTime * Mathf.Sin(counter) * 8, 0, 0);
    }
}
