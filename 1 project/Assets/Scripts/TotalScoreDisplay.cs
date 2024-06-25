using UnityEngine;
using UnityEngine.UI;

public class TotalScoreDisplay : MonoBehaviour
{
    public Text totalScoreText; // Reference to the UI Text component for total score

    void Start()
    {
        // Check if the totalScoreText has been assigned
        if (totalScoreText == null)
        {
            Debug.LogError("Total Score Text component is not assigned!");
            return; // Exit if totalScoreText is not assigned to prevent errors
        }

        // Initially display the total score
        UpdateScoreDisplay();
    }

    void Update()
    {
        // Continuously update the total score display
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        // Load the total score from PlayerPrefs
        int totalScore = PlayerPrefs.GetInt("allscore", 0);

        // Display the total score
        totalScoreText.text = totalScore.ToString();
    }
}
