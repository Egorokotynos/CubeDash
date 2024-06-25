using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkin : MonoBehaviour
{
    public Texture diamondTexture;
    public Texture magmaTexture;
    public Texture goldTexture;
    public Texture spaceTexture;

    private Renderer cubeRenderer;

    void Start()
    {
        // Get the Renderer component from the cube
        cubeRenderer = GetComponent<Renderer>();

        // Apply the texture based on the saved states
        ApplyTexture();

    }
    void Update()
    {
        ApplyTexture();
    }

    private void ApplyTexture()
    {
        bool diamondison = PlayerPrefs.GetInt("diamondison", 0) == 1;
        bool magmaison = PlayerPrefs.GetInt("magmaison", 0) == 1;
        bool goldison = PlayerPrefs.GetInt("goldison", 0) == 1;
        bool spaceison = PlayerPrefs.GetInt("spaceison", 0) == 1;

        if (diamondison)
        {
            cubeRenderer.material.mainTexture = diamondTexture;
        }
        else if (magmaison)
        {
            cubeRenderer.material.mainTexture = magmaTexture;
        }
        else if (goldison)
        {
            cubeRenderer.material.mainTexture = goldTexture;
        }
        else if (spaceison)
        {
            cubeRenderer.material.mainTexture = spaceTexture;
        }
    }
}
