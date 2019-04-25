
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class S2Effect : MonoBehaviour{
    public Material material;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}