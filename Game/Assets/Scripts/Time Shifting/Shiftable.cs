using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiftable : MonoBehaviour
{
    private float localTime = 0;
    public int timeZone;
    // Start is called before the first frame update
    void Start()
    {
        localTime = TimeCore.times[timeZone];
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeCore.check)
        {
            localTime = TimeCore.times[timeZone];
            //attempt to stop all animations
            try
            {
                GetComponent<Animator>().speed = localTime;
            } catch { }
            //attempt to freeze AI
            try
            {
                GetComponent<Controller>().setTime(localTime);
            } catch { }
            //attempt to freeze Particles
            try
            {
                ParticleSystem p = GetComponent<ParticleSystem>();
            }
            catch { }
        }
    }
}
