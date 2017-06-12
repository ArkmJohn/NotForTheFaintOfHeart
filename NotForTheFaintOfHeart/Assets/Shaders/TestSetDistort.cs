using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetDistort : MonoBehaviour {

    public Shader myDistort;

    [Range(0,50)]
    public float intensity;

    public Material _material;
    public Material material
    {
        get
        {
            if (!_material) _material = new Material(myDistort);
            return _material;
        }
    }

    void Awake()
    {
        material.SetFloat("Intensity", intensity);
    }

    void Update()
    {
        if (material.GetFloat("Intensity") != intensity)
        {
            material.SetFloat("Intensity", intensity);

        }
    }

    public virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, material);
    }
}
