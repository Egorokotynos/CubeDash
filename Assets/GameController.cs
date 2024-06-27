using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Button[] ballButtons;
    public Button[] betButtons;
    public Button startButton;
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public GameObject noMoneyMessage;
    public TextMeshProUGUI scoreText;

    private int numberOfBalls;
    private int betAmount;
    private int totalBet;
    private int allScore1;

    void Start()
    {
        Debug.Log("Start method called.");

        allScore1 = PlayerPrefs.GetInt("allscore", 0);
        Debug.Log("Initial allScore1: " + allScore1);

        UpdateUI();

        foreach (Button button in ballButtons)
        {
            button.onClick.AddListener(() => OnBallButtonPressed(button));
        }

        foreach (Button button in betButtons)
        {
            button.onClick.AddListener(() => OnBetButtonPressed(button));
        }

        startButton.onClick.AddListener(OnStartButtonPressed);

        if (noMoneyMessage != null)
        {
            noMoneyMessage.SetActive(false);
        }
    }

    void OnBallButtonPressed(Button pressedButton)
    {
        foreach (Button button in ballButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = buttonText.name;
            }
        }

        TextMeshProUGUI pressedButtonText = pressedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (pressedButtonText != null)
        {
            pressedButtonText.text = "Chosen";
        }

        if (int.TryParse(pressedButton.name, out int result))
        {
            numberOfBalls = result;
            Debug.Log("Number of balls set to: " + numberOfBalls);
        }
    }

    void OnBetButtonPressed(Button pressedButton)
    {
        foreach (Button button in betButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = buttonText.name;
            }
        }

        TextMeshProUGUI pressedButtonText = pressedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (pressedButtonText != null)
        {
            pressedButtonText.text = "Chosen";
        }

        if (int.TryParse(pressedButton.name, out int result))
        {
            betAmount = result;
            Debug.Log("Bet amount set to: " + betAmount);
        }
    }

    void OnStartButtonPressed()
    {
        Debug.Log("Start button pressed.");

        totalBet = numberOfBalls * betAmount;
        Debug.Log("Total bet calculated: " + totalBet);

        if (allScore1 >= totalBet)
        {
            allScore1 -= totalBet;
            Debug.Log("New score after deduction: " + allScore1);
            PlayerPrefs.SetInt("allscore", allScore1);
            PlayerPrefs.Save();
            UpdateUI();

            StartCoroutine(SpawnBalls());
        }
        else
        {
            Debug.Log("Not enough score");
            if (noMoneyMessage != null)
            {
                noMoneyMessage.SetActive(true);
            }
        }
    }

    IEnumerator SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            Debug.Log("Spawning ball " + (i + 1));
            Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + allScore1.ToString();
            Debug.Log("Score updated in UI: " + allScore1);
        }
    }
}
