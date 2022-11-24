using UnityEngine;

public class CamScript : MonoBehaviour
{
    private Material grayMaterial;
    public Shader grayShader;

    private void Start()
    {
        grayMaterial = new Material(grayShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, grayMaterial);
    }
}
