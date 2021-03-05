using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeControls : MonoBehaviour
{
    private PlayerAudio playerAudio;
    private bool canShift = true;
    private Volume volume;
    private LensDistortion distortion;
    private ColorAdjustments colors;
    private ChromaticAberration chromatic;
    private int currentTimeZone = 0;
    //private Vignette vignette;

    private float chromaticInit = 0f;
    //private float vignetteInit = 0f;
    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        //postProcessing = FindObjectOfType<Volume>().GetComponent<Animator>();
        volume = FindObjectOfType<Volume>();

        if (volume.profile.TryGet<LensDistortion>(out var lens))
            distortion = lens;
        if (volume.profile.TryGet<ColorAdjustments>(out var postColors))
            colors = postColors;
        if (volume.profile.TryGet<ChromaticAberration>(out var chrom))
        {
            chromatic = chrom;
            chromaticInit = chrom.intensity.value;
        }

        /*
        if (volume.profile.TryGet<Vignette>(out var vin))
        {
            vignette = vin;
            vignetteInit = vin.intensity.value;
        }*/
    }
    void Update()
    {
        if (canShift)
        {
            if (Input.GetButtonDown("Time1") && currentTimeZone!=1)
            {
                currentTimeZone = 1;
                playerAudio.PlayWarp(1);
                DebugTime(1);
                TimeCore.Shift(0);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }
            else if (Input.GetButtonDown("Time2") && currentTimeZone != 2)
            {
                currentTimeZone = 2;
                playerAudio.PlayWarp(2);
                DebugTime(2);
                TimeCore.Shift(1);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }
            else if (Input.GetButtonDown("Time3") && currentTimeZone != 3)
            {
                currentTimeZone = 3;
                playerAudio.PlayWarp(3);
                DebugTime(3);
                TimeCore.Shift(2);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }
            else if (Input.GetButtonDown("Time4") && currentTimeZone != 4)
            {
                currentTimeZone = 4;
                playerAudio.PlayWarp(4);
                DebugTime(4);
                TimeCore.Shift(3);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }

            ///////////////scroll wheel
            else if (Input.GetAxis("Time3MouseWheel") > 0 && currentTimeZone != 3)
            {
                currentTimeZone = 3;
                playerAudio.PlayWarp(3);
                DebugTime(3);
                TimeCore.Shift(2);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }
            else if (Input.GetAxis("Time4MouseWheel") < 0 && currentTimeZone != 4)
            {
                currentTimeZone = 4;
                playerAudio.PlayWarp(4);
                DebugTime(4);
                TimeCore.Shift(3);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift());
            }
        }
    }

    IEnumerator DistortForTimeShift()
    {
        while (distortion.intensity.value > -1f)
        {
            distortion.intensity.value -= Time.deltaTime * 6;

            colors.hueShift.value -= Time.deltaTime * 1000;
            colors.hueShift.value = Mathf.Clamp(colors.hueShift.value, -180, 0);

            chromatic.intensity.value += Time.deltaTime * 4;
            chromatic.intensity.value = Mathf.Clamp(chromatic.intensity.value, chromaticInit, 1);

            //vignette.intensity.value += Time.deltaTime;
            //vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, vignetteInit, 1);
            yield return new WaitForEndOfFrame();
        }
        //colors.hueShift.value = -180;
        while (distortion.intensity.value < 0)
        {
            distortion.intensity.value += Time.deltaTime * 4;
            colors.hueShift.value += Time.deltaTime * 1000;
            colors.hueShift.value = Mathf.Clamp(colors.hueShift.value, -180, 0);

            chromatic.intensity.value -= Time.deltaTime * 8;
            chromatic.intensity.value = Mathf.Clamp(chromatic.intensity.value, chromaticInit, 1);

            //vignette.intensity.value -= Time.deltaTime;
            //vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, vignetteInit, 1);
            yield return new WaitForEndOfFrame();
        }

        colors.hueShift.value = 0;
        //vignette.intensity.value = vignetteInit;


    }

    private IEnumerator Shift()
    {
        canShift = false;
        yield return new WaitForSeconds(0.5f);
        canShift = true;
    }

    private void DebugTime(int time)
    {
        Debug.Log("Current Time Zone = <color=cyan>" + time + "</color>");
    }
}
