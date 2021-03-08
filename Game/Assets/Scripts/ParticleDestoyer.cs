using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoyer : MonoBehaviour
{
    public IEnumerator DestroyThis(float time = 1f)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void DestroyParticle(float time = 1f)
    {
        //var main = GetComponent<ParticleSystem>().main;
        //main.simulationSpeed = 0;
        StartCoroutine(DestroyThis(time));
    }
}
