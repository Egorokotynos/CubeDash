using UnityEngine;
using UnityEngine.UI;

public class PlinkoManager : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab to be spawned
    public Transform dropPosition; // Position where the prefabs will be instantiated

    // Public references to the buttons
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    // Reference to the Text element to display the score
    public Text scoreText;

    // Object to activate when the player doesn't have enough points
    public GameObject objectToActivate;

    private int allScore;

    void Start()
    {
        // Initialize score
        allScore = PlayerPrefs.GetInt("allscore", 0);
        UpdateScoreText();

        // Add listeners to the buttons
        button1.onClick.AddListener(() => TryDropPrefabs(1));
        button2.onClick.AddListener(() => TryDropPrefabs(2));
        button3.onClick.AddListener(() => TryDropPrefabs(3));
        button4.onClick.AddListener(() => TryDropPrefabs(4));
    }

    void TryDropPrefabs(int count)
    {
        int totalCost = 10 * count;

        if (allScore >= totalCost)
        {
            DropPrefabs(count);
        }
        else
        {
            ActivateObject();
        }
    }

    void DropPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, dropPosition.position, Quaternion.identity);
        }

        // Subtract points for each prefab dropped
        allScore -= 10 * count;
        PlayerPrefs.SetInt("allscore", allScore);
        PlayerPrefs.Save(); // Ensure the score is saved

        UpdateScoreText();
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + allScore;
    }
}
