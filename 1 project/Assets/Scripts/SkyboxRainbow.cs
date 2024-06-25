using UnityEngine;

public class SkyboxRainbow : MonoBehaviour
{
    public float duration = 5.0f; // Время полного круга изменения цвета

    private Material skyboxMaterial;
    private float hue = 0f;

    void Start()
    {
        // Получаем текущий материал скайбокса
        if (RenderSettings.skybox != null)
        {
            skyboxMaterial = RenderSettings.skybox;
        }
    }

    void Update()
    {
        if (skyboxMaterial != null)
        {
            // Увеличиваем значение оттенка (Hue) со временем
            hue += Time.deltaTime / duration;
            if (hue > 1f)
            {
                hue -= 1f;
            }

            // Преобразуем HSV в RGB
            Color currentColor = Color.HSVToRGB(hue, 1f, 1f);

            // Устанавливаем цвет в материал скайбокса
            skyboxMaterial.SetColor("_Tint", currentColor);
        }
    }
}
