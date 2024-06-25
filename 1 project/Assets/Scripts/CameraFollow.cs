using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target object the camera will follow
    public Transform target;

    // The distance in the x-z plane to the target
    public float distance = 10.0f;

    // The height the camera will be above the target
    public float height = 5.0f;

    // Damping for smooth movement
    public float heightDamping = 2.0f;
    public float positionDamping = 2.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        // Early exit if there’s no target
        if (!target) return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, positionDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);
    }
}
