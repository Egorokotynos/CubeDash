using UnityEngine;

public class GraphicsController : MonoBehaviour
{
    private const string GraphicsQualityKey = "GraphicsQuality";

    void Start()
    {
        // При запуске игры применяем сохраненные настройки качества графики
        ApplyGraphicsQuality();
    }

    public void SetGoodGraphics()
    {
        // Устанавливаем качество графики на высокое
        QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);

        // Сохраняем выбранное качество графики
        PlayerPrefs.SetInt(GraphicsQualityKey, QualitySettings.GetQualityLevel());
    }

    public void SetBadGraphics()
    {
        // Устанавливаем качество графики на низкое
        QualitySettings.SetQualityLevel(0);

        // Сохраняем выбранное качество графики
        PlayerPrefs.SetInt(GraphicsQualityKey, QualitySettings.GetQualityLevel());
    }

    private void ApplyGraphicsQuality()
    {
        // Загружаем ранее сохраненное качество графики и применяем его
        int savedQuality = PlayerPrefs.GetInt(GraphicsQualityKey, QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(savedQuality);
    }
}
