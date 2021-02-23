using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCore : MonoBehaviour
{
    public static float [] times = {1, 0, 0, 0};
    private static int cd = 2;
    public static bool check = true;

    private void Update()
    {
        if(cd > 0)
        {
            cd--;
            if(cd == 0)
            {
                check = false;
            }
        }
    }
    public static void shift(int id)
    {
        for(int i = 0; i < times.Length; i++)
        {
            int j = 0;
            if(i == id)
            {
                j = 1;
            }
            times[i] = j;
        }
        cd = 2;
        check = true;
    }
}
