using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiftable : MonoBehaviour
{
    private float localTime = 0;
    public int timeZone;
    public Material active, stopped;
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
            } 
            catch 
            {
                try
                {
                    GetComponentInChildren<Animator>().speed = localTime;
                }
                catch
                {

                }
            }
            //attempt to freeze AI
            try
            {
                GetComponent<Controller>().setTime(localTime);
            } catch { }
            //attempt to freeze Particles
            try
            {

            }
            catch { }
            if (localTime > 0)
            {
                try
                {
                    GetComponent<MeshRenderer>().material = active;
                }
                catch
                {
                    try
                    {
                        var meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
                        if (meshes.Length == 0)
                        {
                            foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                            {
                                m.material = active;
                            }
                        }
                        else
                        {
                            if (GetComponent<Algro>() != null)
                            {
                                foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                                {
                                    m.material = active;
                                }
                            }
                            else
                            {
                                foreach (SkinnedMeshRenderer m in meshes)
                                {
                                    m.material = active;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                try
                {
                    GetComponent<MeshRenderer>().material = stopped;
                }
                catch
                {
                    try
                    {
                        var meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
                        if (meshes.Length == 0)
                        {
                            foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                            {
                                m.material = stopped;
                            }
                        }
                        else
                        {
                            if (GetComponent<Algro>() != null)
                            {
                                foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                                {
                                    m.material = stopped;
                                }
                            }
                            else
                            {
                                foreach (SkinnedMeshRenderer m in meshes)
                                {
                                    m.material = stopped;
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }

        }
    }
}
