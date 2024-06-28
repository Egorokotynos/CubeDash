using System;
using CubeDash.Assets.Scripts.IosMessagesSKD;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public AudioSource soundPlay;
    public float speed = 1.0f; // Speed at which the object moves
    public float jumpForce = 1.0f; // The force applied when the cube jumps
    public float jumpCooldown = 1.0f; // Cooldown period between jumps
    public GameObject objectToActivate; // The object to activate on collision

    private Rigidbody rb;
    private float timeSinceLastJump;

    private void OnEnable()
    {
        if (NotificationCustomCenter.Instance)
            NotificationCustomCenter.Instance.AddObserver("ADLOVIN_SHOW_AD", ShowAd);
    }

    private void ShowAd()
    {
        Debug.Log("add Show");
    }
    private void OnDisable()
    {
        NotificationCustomCenter.Instance.RemoveObserver("ADLOVIN_SHOW_AD", ShowAd);
    }
    void Start()
    {
        // Get the Rigidbody component attached to the cube
        rb = GetComponent<Rigidbody>();
        // Initialize the timeSinceLastJump
        timeSinceLastJump = jumpCooldown; // Start with the cube being able to jump immediately

        // Ensure the object to activate is initially inactive
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void Update()
    {
        // Update the cooldown timer
        timeSinceLastJump += Time.deltaTime;

        // Log the current position
        Debug.Log("Current Position: " + transform.position);

        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(0, 0, 10) * speed * Time.deltaTime;

        // Update the object's position
        transform.position = newPosition;

        // Log the new position
        Debug.Log("New Position: " + transform.position);

        // Check for touch input or mouse click and if the cooldown period has passed
        if (Input.GetMouseButtonDown(0) && timeSinceLastJump >= jumpCooldown)
        {
            // Apply upward force to the cube
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Reset the cooldown timer
            timeSinceLastJump = 0f;
            soundPlay.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Spike"
        if (collision.gameObject.tag == "Spike")
        {
            // Activate the chosen object
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);

                NotificationCustomCenter.Instance.PostNotification("ADLOVIN_SHOW_AD");
            }
        }
    }
}
