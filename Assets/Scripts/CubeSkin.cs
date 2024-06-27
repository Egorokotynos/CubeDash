using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkin : MonoBehaviour
{
    public Texture diamondTexture;
    public Texture magmaTexture;
    public Texture goldTexture;
    public Texture spaceTexture;
    public Texture skin1Texture; // Texture for skin1
    public Texture skin2Texture; // Texture for skin2

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
        bool skin1ison = PlayerPrefs.GetInt("skin1ison", 0) == 1;
        bool skin2ison = PlayerPrefs.GetInt("skin2ison", 0) == 1;

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
        else if (skin1ison)
        {
            cubeRenderer.material.mainTexture = skin1Texture;
        }
        else if (skin2ison)
        {
            cubeRenderer.material.mainTexture = skin2Texture;
        }
    }
}
