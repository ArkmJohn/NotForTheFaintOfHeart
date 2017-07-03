using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Custom/ColorGrade")]
public class ColorGradeApply : MonoBehaviour
{
    public Shader shader;
    private Material material;
    public Texture3D convertedLUT = null;

    public void SetLUT()
    {
        int dim = 16;
        var newC = new Color[dim * dim * dim];
        float oneOverDim = 1.0f / (1.0f * dim - 1.0f);

        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                for (int k = 0; k < dim; k++)
                {
                    newC[i + (j * dim) + (k * dim * dim)] = new Color((i * 1.0f) * oneOverDim, (j * 1.0f) * oneOverDim, (k * 1.0f) * oneOverDim, 1.0f);
                }
            }
        }
        convertedLUT = new Texture3D(dim, dim, dim, TextureFormat.ARGB32, false);
        convertedLUT.SetPixels(newC);
        convertedLUT.Apply();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (convertedLUT == null)
        {
            SetLUT();
        }
        material.SetTexture("_ClutTex", convertedLUT);
        Graphics.Blit(source, destination, material, QualitySettings.activeColorSpace == ColorSpace.Linear ? 1 : 0);
    }

}
