using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Slider qualitySlider;

    void Start()
    {
        // ������������� �������� �������� � ��������� �� 0 �� 2, ��� ��� � ��� ��� ������ ��������
        qualitySlider.minValue = 0;
        qualitySlider.maxValue = 2;
        qualitySlider.wholeNumbers = true; // ������� ����� ��������� ������ ����� ��������

        // ����������� �������� �������� �������� �������� �������
        qualitySlider.value = QualitySettings.GetQualityLevel();

        // ��������� ��������� ��� ��������� �������� ��������
        qualitySlider.onValueChanged.AddListener(delegate { ChangeGraphicsQuality(); });
    }

    // ����� ��� ��������� �������� �������
    void ChangeGraphicsQuality()
    {
        int qualityLevel = Mathf.RoundToInt(qualitySlider.value);
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
}
