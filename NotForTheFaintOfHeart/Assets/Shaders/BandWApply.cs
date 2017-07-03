﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Custom/BandW")]
public class BandWApply : MonoBehaviour
{
    public Shader bandw;
    public Texture textRamp;
    private Material material;

    void Awake()
    {
        if(material == null)
            material = new Material(bandw);

    }

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_RampTex", textRamp);
        Graphics.Blit(source, destination, material);
    }
}
