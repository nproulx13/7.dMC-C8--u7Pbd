using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCore : MonoBehaviour
{
    public static float [] times = {0, 1, 1, 1};
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
    public static void Shift(int id)
    {
        for(int i = 0; i < times.Length; i++)
        {
            int j = 1;
            if(i == id)
            {
                j = 0;
            }
            times[i] = j;
        }
        cd = 2;
        check = true;
    }
}
