using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Slider qualitySlider;

    void Start()
    {
        // ”станавливаем значени€ слайдера в диапазоне от 0 до 2, так как у нас три уровн€ качества
        qualitySlider.minValue = 0;
        qualitySlider.maxValue = 2;
        qualitySlider.wholeNumbers = true; // —лайдер будет принимать только целые значени€

        // ѕрисваиваем значение слайдера текущему качеству графики
        qualitySlider.value = QualitySettings.GetQualityLevel();

        // ƒобавл€ем слушатель дл€ изменений значени€ слайдера
        qualitySlider.onValueChanged.AddListener(delegate { ChangeGraphicsQuality(); });
    }

    // ћетод дл€ изменени€ качества графики
    void ChangeGraphicsQuality()
    {
        int qualityLevel = Mathf.RoundToInt(qualitySlider.value);
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
}
