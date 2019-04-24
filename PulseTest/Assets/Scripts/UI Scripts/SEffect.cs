using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SEffect : MonoBehaviour {
 
 public float mix;
 private Material material;
 
 // Creates a private material used to the effect
 void Awake ()
 {
 material = new Material( Shader.Find("Custom/Edge") );
 }
 
 // Postprocess the image
 void OnRenderImage (RenderTexture source, RenderTexture destination){
    if (mix == 0)
    {
        Graphics.Blit (source, destination);
        return;
    }
 
    material.SetFloat("_Mix", mix);
    Graphics.Blit (source, destination, material);
 }
}