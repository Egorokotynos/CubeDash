using UnityEngine;

public class ScoreIncrementer : MonoBehaviour
{
    // Ключ для хранения общего счета в PlayerPrefs
    private const string allScoreKey = "allscore";

    // Метод для увеличения счета с учетом множителя скина
    public void IncrementScore()
    {
        // Получаем текущий счет, по умолчанию 0, если не установлен
        int currentScore = PlayerPrefs.GetInt(allScoreKey, 0);

        // Получаем множитель скина на основе экипированных скинов
        float totalMultiplier = CalculateTotalMultiplier();

        // Увеличиваем счет с учетом общего множителя
        currentScore += Mathf.RoundToInt(1 * totalMultiplier);

        // Сохраняем обновленный счет в PlayerPrefs
        PlayerPrefs.SetInt(allScoreKey, currentScore);

        // Опционально, выводим новый счет в консоль
        Debug.Log("New allscore: " + currentScore);
    }

    // Метод для вычисления общего множителя на основе экипированных скинов
    private float CalculateTotalMultiplier()
    {
        float totalMultiplier = 1.0f; // Начальное значение общего множителя

        // Проверяем каждый из скинов и добавляем их множители, если они экипированы
        if (PlayerPrefs.GetInt("diamondison", 0) == 1)
        {
            totalMultiplier *= 2.0f; // Множитель для diamondison
        }
        if (PlayerPrefs.GetInt("magmaison", 0) == 1)
        {
            totalMultiplier *= 5.0f; // Множитель для magmaison
        }
        if (PlayerPrefs.GetInt("goldison", 0) == 1)
        {
            totalMultiplier *= 10.0f; // Множитель для goldison
        }
        if (PlayerPrefs.GetInt("spaceison", 0) == 1)
        {
            totalMultiplier *= 20.0f; // Множитель для spaceison
        }

        return totalMultiplier;
    }
}
