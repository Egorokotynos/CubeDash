using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text component
    private int score = 0; // Initial score
    private int allscore; // Persistent score

    void Start()
    {
        // Check if the scoreText has been assigned
        if (scoreText == null)
        {
            Debug.LogError("Score Text component is not assigned!");
            return; // Exit if scoreText is not assigned to prevent errors
        }

        // Load the allscore from PlayerPrefs
        allscore = PlayerPrefs.GetInt("allscore", 0);

        // Initialize the score text
        UpdateScoreText();

        // Call AddPoint every second
        InvokeRepeating("AddPoint", 1f, 1f);
    }

    // Call this method to increment the score
    public void AddPoint()
    {
        score += 100;
        allscore += 100;

        // Save the updated allscore to PlayerPrefs
        PlayerPrefs.SetInt("allscore", allscore);

        UpdateScoreText();
    }

    // Updates the score text to reflect the current score
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString() ;
        }
    }
}
