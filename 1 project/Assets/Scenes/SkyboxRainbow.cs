using UnityEngine;

public class SkyboxRainbow : MonoBehaviour
{
    public float duration = 5.0f; // ����� ������� ����� ��������� �����

    private Material skyboxMaterial;
    private float hue = 0f;

    void Start()
    {
        // �������� ������� �������� ���������
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }
    }

    void Update()
    {
        if (skyboxMaterial != null)
        {
            // ����������� �������� ������� (Hue) �� ��������
            hue += Time.deltaTime / duration;
            if (hue > 1f)
            {
                hue -= 1f;
            }

            // ����������� HSV � RGB
            Color currentColor = Color.HSVToRGB(hue, 1f, 1f);

            // ������������� ���� � �������� ���������
            skyboxMaterial.SetColor("_Tint", currentColor);
        }
    }
}
