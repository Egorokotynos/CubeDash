using UnityEngine;

public class ScoreIncrementer : MonoBehaviour
{
    // ���� ��� �������� ������ ����� � PlayerPrefs
    private const string allScoreKey = "allscore";

    // ����� ��� ���������� ����� � ������ ��������� �����
    public void IncrementScore()
    {
        // �������� ������� ����, �� ��������� 0, ���� �� ����������
        int currentScore = PlayerPrefs.GetInt(allScoreKey, 0);

        // �������� ��������� ����� �� ������ ������������� ������
        float totalMultiplier = CalculateTotalMultiplier();

        // ����������� ���� � ������ ������ ���������
        currentScore += Mathf.RoundToInt(1 * totalMultiplier);

        // ��������� ����������� ���� � PlayerPrefs
        PlayerPrefs.SetInt(allScoreKey, currentScore);

        // �����������, ������� ����� ���� � �������
        Debug.Log("New allscore: " + currentScore);
    }

    // ����� ��� ���������� ������ ��������� �� ������ ������������� ������
    private float CalculateTotalMultiplier()
    {
        float totalMultiplier = 1.0f; // ��������� �������� ������ ���������

        // ��������� ������ �� ������ � ��������� �� ���������, ���� ��� �����������
        if (PlayerPrefs.GetInt("diamondison", 0) == 1)
        {
            totalMultiplier *= 2.0f; // ��������� ��� diamondison
        }
        if (PlayerPrefs.GetInt("magmaison", 0) == 1)
        {
            totalMultiplier *= 5.0f; // ��������� ��� magmaison
        }
        if (PlayerPrefs.GetInt("goldison", 0) == 1)
        {
            totalMultiplier *= 10.0f; // ��������� ��� goldison
        }
        if (PlayerPrefs.GetInt("spaceison", 0) == 1)
        {
            totalMultiplier *= 20.0f; // ��������� ��� spaceison
        }

        return totalMultiplier;
    }
}
