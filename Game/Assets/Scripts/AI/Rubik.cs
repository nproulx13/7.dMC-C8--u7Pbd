using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubik : MonoBehaviour
{
    public float counter = 2f, rot = 0f;
    public float last = 0;
    private int i;
    void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else if (rot <= 0)
        {
            i = Random.Range(-1, 3);
            last = GetComponent<Rigidbody>().rotation.eulerAngles.x;
            rot = .5f;
        }
        else
        {
            GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, (i * 90 * rot * 2) + last, 0));
            rot -= Time.deltaTime;
            if (rot <= 0)
            {
                counter = 2;
            }
        }
    }
}
