using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Custom/Color Correct Curve")]
public class ColorCorrectCurve : MonoBehaviour
{
    public Shader shader;
    private Material ccMaterial;

    public AnimationCurve redChannel = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public AnimationCurve greenChannel = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public AnimationCurve blueChannel = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public float saturation = 1.0f;

    private Texture2D rgbChannelTex;

    public void UpdateParams()
    {
        if (!rgbChannelTex)
            rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);

        for (float i = 0.0f; i <= 1.0f; i += 1.0f / 255.0f)
        {
            float rCh = Mathf.Clamp(redChannel.Evaluate(i), 0.0f, 1.0f);
            float gCh = Mathf.Clamp(greenChannel.Evaluate(i), 0.0f, 1.0f);
            float bCh = Mathf.Clamp(blueChannel.Evaluate(i), 0.0f, 1.0f);

            rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 0, new Color(rCh, rCh, rCh));
            rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 1, new Color(gCh, gCh, gCh));
            rgbChannelTex.SetPixel((int)Mathf.Floor(i * 255.0f), 2, new Color(bCh, bCh, bCh));
        }

        rgbChannelTex.Apply();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture renderTarget2Use = destination;

        ccMaterial.SetTexture("_RgbTex", rgbChannelTex);
        ccMaterial.SetFloat("_Saturation", saturation);

        Graphics.Blit(source, renderTarget2Use, ccMaterial);
    }
}