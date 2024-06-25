using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopSETTING : MonoBehaviour
{
    // The GameObject you want to toggle
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        // Check for user input to toggle the object
        // For example, press the "T" key to toggle
        if (Input.GetKeyDown(KeyCode.T))
        {
            Toggle();
        }
    }

    // Method to toggle the target object's active state
    public void Toggle()
    {
        if (targetObject != null)
        {
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);
            Debug.Log($"{targetObject.name} is now {(isActive ? "OFF" : "ON")}");
        }
    }

    // Method to explicitly turn on the target object
    public void TurnOn()
    {
        if (targetObject != null && !targetObject.activeSelf)
        {
            targetObject.SetActive(true);
            Debug.Log($"{targetObject.name} is now ON");
        }
    }

    // Method to explicitly turn off the target object
    public void TurnOff()
    {
        if (targetObject != null && targetObject.activeSelf)
        {
            targetObject.SetActive(false);
            Debug.Log($"{targetObject.name} is now OFF");
        }
    }
}
