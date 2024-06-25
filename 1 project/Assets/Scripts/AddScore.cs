using UnityEngine;

public class AddScore : MonoBehaviour
{
    // Method to add 1000 to the "allscore" PlayerPrefs key
    public void AddToAllScore()
    {
        // Get the current score value
        int currentScore = PlayerPrefs.GetInt("allscore", 0);

        // Add 1000 to the current score
        int newScore = currentScore + 1000;

        // Save the new score back to PlayerPrefs
        PlayerPrefs.SetInt("allscore", newScore);

        // Optionally, log the new score for debugging purposes
        Debug.Log("New allscore: " + newScore);
    }
}
