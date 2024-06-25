using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomTextureChanger : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Sprite[] sprites;
    public Button startButton;
    public GameObject winPrefab; // Prefab to spawn on win (3 out of 3 same)
    public GameObject twoOutOfThreePrefab; // Prefab to spawn on 2 out of 3 same
    public Transform spawnPoint; // Point to spawn the prefab
    public AudioClip winSound; // Sound to play on win
    public AudioClip rollSound; // Sound to play while rolling
    public AudioClip twoOutOfThreeSound; // Sound to play when 2 out of 3 images match
    public GameObject winObject; // Object to activate on win
    public GameObject noMoneyObject; // Object to activate if not enough points
    private AudioSource audioSource; // Audio source to play the sound

    private Coroutine textureCoroutine;
    private bool isChanging = false;

    void Start()
    {
        if (sprites.Length < 5)
        {
            Debug.LogError("Please assign at least 5 sprites in the Inspector.");
            return;
        }

        startButton.onClick.AddListener(StartChangingTextures);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void StartChangingTextures()
    {
        int currentPoints = PlayerPrefs.GetInt("allscore", 0);
        if (currentPoints < 30)
        {
            Debug.Log("Not enough points to roll.");
            if (noMoneyObject != null)
            {
                noMoneyObject.SetActive(true);
            }
            return;
        }

        if (!isChanging)
        {
            DeductPointsForRoll();
            if (textureCoroutine != null)
            {
                StopCoroutine(textureCoroutine);
            }
            textureCoroutine = StartCoroutine(ChangeTexturesForDuration(2f));
        }
    }

    IEnumerator ChangeTexturesForDuration(float duration)
    {
        isChanging = true;
        float elapsedTime = 0f;

        // Play the rolling sound
        if (rollSound != null)
        {
            audioSource.clip = rollSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        while (elapsedTime < duration)
        {
            ChangeTextures();
            yield return new WaitForSeconds(0.05f); // Change textures every 0.05 seconds
            elapsedTime += 0.05f;
        }

        // Stop the rolling sound
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Small delay to ensure the last image has stopped before checking
        yield return new WaitForSeconds(0.5f);

        CheckAndAddPoints();
        isChanging = false;
    }

    void ChangeTextures()
    {
        float chance = Random.Range(0f, 1f);

        if (chance < 0.10f) // 10% chance
        {
            SetTwoOutOfThreeTextures();
        }
        else
        {
            SetRandomTextures();
        }
    }

    void SetRandomTextures()
    {
        image1.sprite = sprites[Random.Range(0, sprites.Length)];
        image2.sprite = sprites[Random.Range(0, sprites.Length)];
        image3.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void SetTwoOutOfThreeTextures()
    {
        int commonIndex = Random.Range(0, sprites.Length);
        int differentIndex;
        do
        {
            differentIndex = Random.Range(0, sprites.Length);
        } while (differentIndex == commonIndex);

        int slot = Random.Range(0, 3);
        if (slot == 0)
        {
            image1.sprite = sprites[commonIndex];
            image2.sprite = sprites[commonIndex];
            image3.sprite = sprites[differentIndex];
        }
        else if (slot == 1)
        {
            image1.sprite = sprites[commonIndex];
            image2.sprite = sprites[differentIndex];
            image3.sprite = sprites[commonIndex];
        }
        else
        {
            image1.sprite = sprites[differentIndex];
            image2.sprite = sprites[commonIndex];
            image3.sprite = sprites[commonIndex];
        }
    }

    void CheckAndAddPoints()
    {
        bool allEqual = image1.sprite == image2.sprite && image2.sprite == image3.sprite;
        bool twoOutOfThreeEqual = (image1.sprite == image2.sprite && image1.sprite != image3.sprite) ||
                                  (image1.sprite == image3.sprite && image1.sprite != image2.sprite) ||
                                  (image2.sprite == image3.sprite && image2.sprite != image1.sprite);

        if (allEqual)
        {
            int currentPoints = PlayerPrefs.GetInt("allscore", 0);
            PlayerPrefs.SetInt("allscore", currentPoints + 1000); // Add 1000 points
            PlayerPrefs.Save();

            // Spawn the win prefab
            if (winPrefab != null && spawnPoint != null)
            {
                GameObject spawnedPrefab = Instantiate(winPrefab, spawnPoint.position, spawnPoint.rotation);
                Destroy(spawnedPrefab, 5f); // Destroy the spawned prefab after 5 seconds
            }

            // Play the win sound
            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
            }

            // Activate the win object
            if (winObject != null)
            {
                winObject.SetActive(true);
            }
        }
        else if (twoOutOfThreeEqual)
        {
            int currentPoints = PlayerPrefs.GetInt("allscore", 0);
            PlayerPrefs.SetInt("allscore", currentPoints + 40); 
            PlayerPrefs.Save();

            // Spawn the two-out-of-three prefab
            if (twoOutOfThreePrefab != null && spawnPoint != null)
            {
                GameObject spawnedPrefab = Instantiate(twoOutOfThreePrefab, spawnPoint.position, spawnPoint.rotation);
                Destroy(spawnedPrefab, 3f); // Destroy the spawned prefab after 5 seconds
            }

            // Play the two-out-of-three sound
            if (twoOutOfThreeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(twoOutOfThreeSound);
            }
        }
    }

    void DeductPointsForRoll()
    {
        int currentPoints = PlayerPrefs.GetInt("allscore", 0);
        PlayerPrefs.SetInt("allscore", currentPoints - 30);
        PlayerPrefs.Save();
    }
}
