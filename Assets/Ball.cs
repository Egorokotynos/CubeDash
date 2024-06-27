using UnityEngine;

public class Ball : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] collisionSounds;
    public float bounceForce = 2.0f; // The amount of random force to apply on collision
    public float offset = 2f;
    public Vector2 spawnPosition1;
    public Vector2 spawnPosition2;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the ball.");
        }

        if (collisionSounds.Length < 5)
        {
            Debug.LogWarning("Please assign at least 5 audio clips for the collision sounds.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float multiplier = 0;
        AudioClip soundToPlay = null;

        // Check the tag of the collider and assign the corresponding sound
        if (collision.collider.CompareTag("Trigger1"))
        {
            multiplier = 0.2f;
            soundToPlay = GetSound(0);
        }
        else if (collision.collider.CompareTag("Trigger2"))
        {
            multiplier = 0.5f;
            soundToPlay = GetSound(1);
        }
        else if (collision.collider.CompareTag("Trigger3"))
        {
            multiplier = 3f;
            soundToPlay = GetSound(2);
        }
        else if (collision.collider.CompareTag("Trigger4"))
        {
            multiplier = 3f;
            soundToPlay = GetSound(3);
        }
        else if (collision.collider.CompareTag("Trigger5"))
        {
            multiplier = 0.5f;
            soundToPlay = GetSound(4);
        }
        else if (collision.collider.CompareTag("Double"))
        {
            GameObject newBall1 = Instantiate(gameObject, transform.position + new Vector3(-offset, 0f, 0f), Quaternion.identity);
            GameObject newBall2 = Instantiate(gameObject, transform.position + new Vector3(offset, 0f, 0f), Quaternion.identity);
        }
        else
        {
            // Apply random bounce for untagged collisions as well
            ApplyRandomBounce();
            return;
        }

        // Play the corresponding sound if assigned
        if (audioSource != null && soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }

        if (multiplier > 0)
        {
            int ballValue = 10; // Define how you get the ball value
            int scoreToAdd = Mathf.RoundToInt(ballValue * multiplier);
            int currentScore = PlayerPrefs.GetInt("allscore");
            PlayerPrefs.SetInt("allscore", currentScore + scoreToAdd);
            Debug.Log("Score Added: " + scoreToAdd);
        }

        ApplyRandomBounce();

        // Destroy the ball on collision with tagged objects
        Destroy(gameObject);
    }

    private void ApplyRandomBounce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply a random force in the x direction and a positive force in the y direction
            Vector2 randomForce = new Vector2(Random.Range(-bounceForce, bounceForce), Random.Range(0, bounceForce));
            rb.AddForce(randomForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the ball.");
        }
    }

    private AudioClip GetSound(int index)
    {
        if (index >= 0 && index < collisionSounds.Length)
        {
            return collisionSounds[index];
        }
        else
        {
            Debug.LogWarning("Sound index " + index + " is out of bounds. Make sure to assign enough audio clips.");
            return null;
        }
    }
}
