using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class HDRPDayCycle : MonoBehaviour
{
    [Range(0, 24)] public float tOD;
    public float orbitSpeed = 1f;
    public Light sun;
    public Light moon;
    private bool isNight;
    public Volume skyVolume;
    private PhysicallyBasedSky sky;
    public AnimationCurve starsCurve;

    private void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }

    void Update()
    {
        tOD += Time.deltaTime * orbitSpeed;
        if (tOD > 24)
        {
            tOD = 0;
        }
        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = tOD / 24f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150f, 0);

       sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 10;
        
        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }

    void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }
}
