
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class S2Effect : MonoBehaviour{
    public Material material;
    public float gradient;
    public float colorT;
    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_GradThresh",gradient);
        material.SetFloat("_ColorThreshold",colorT);
        Graphics.Blit(source, destination, material);
    }
}