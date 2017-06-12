﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
#endif
public class GlitchEffect : ImageEffectBase
{
    public Texture2D displaceMap;
    float glitchup, glitchdown, flicker,
            glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;

    [Header("Glitch Intensity")]

    [Range(0, 1)]
    public float intensity;

    [Range(0, 1)]
    public float flipIntensity;

    [Range(0, 1)]
    public float colorIntensity;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        material.SetFloat("_Intensity", intensity);
        material.SetFloat("_ColorIntensity", colorIntensity);
        material.SetTexture("_Displacement", displaceMap);

        flicker += Time.deltaTime * colorIntensity;
        if (flicker > flickerTime)
        {
            material.SetFloat("filterRad", Random.Range(-3f, 3f) * colorIntensity);
            material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
            flicker = 0;
            flickerTime = Random.value;
        }

        if (colorIntensity == 0)
            material.SetFloat("filterRad", 0);

        glitchup += Time.deltaTime * flipIntensity;
        if (glitchup > glitchupTime)
        {
            if (Random.value < 0.1f * flipIntensity)
                material.SetFloat("fUp", Random.Range(0, 1f) * flipIntensity);
            else
                material.SetFloat("fUp", 0);

            glitchup = 0;
            glitchupTime = Random.value / 10f;
        }

        if (flipIntensity == 0)
            material.SetFloat("fUp", 0);


        glitchdown += Time.deltaTime * flipIntensity;
        if (glitchdown > glitchdownTime)
        {
            if (Random.value < 0.1f * flipIntensity)
                material.SetFloat("fDown", 1 - Random.Range(0, 1f) * flipIntensity);
            else
                material.SetFloat("fDown", 1);

            glitchdown = 0;
            glitchdownTime = Random.value / 10f;
        }

        if (flipIntensity == 0)
            material.SetFloat("fDown", 1);

        if (Random.value < 0.05 * intensity)
        {
            material.SetFloat("disp", Random.value * intensity);
            material.SetFloat("scale", 1 - Random.value * intensity);
        }
        else
            material.SetFloat("disp", 0);

        Graphics.Blit(source, destination, material);
    }
}