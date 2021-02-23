using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControls : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Time1"))
        {
            TimeCore.shift(0);
        }
        else if(Input.GetButtonDown("Time2"))
        {
            TimeCore.shift(1);
        }
        else if (Input.GetButtonDown("Time3"))
        {
            TimeCore.shift(2);
        }
        else if (Input.GetButtonDown("Time4"))
        {
            TimeCore.shift(3);
        }
    }
}
