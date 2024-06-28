using UnityEngine;
using UnityEngine.UI;

public class ObjectToggle : MonoBehaviour
{
    public GameObject objectToToggle;

    void Start()
    {
        // Assuming you have a button with an OnClick event that calls this method
        Button button = GetComponent<Button>();
        if (button)
            button.onClick.AddListener(ToggleObject);
    }

    public void ToggleObject()
    {
        // Check if the object to toggle is valid
        if (objectToToggle != null)
        {
            // Toggle the active state of the object
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
        else
        {
            Debug.LogWarning("No object assigned to ObjectToggle script.");
        }
    }
}
