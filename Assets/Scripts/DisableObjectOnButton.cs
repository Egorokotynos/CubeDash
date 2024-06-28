using UnityEngine;

public class DisableObjectOnButton : MonoBehaviour
{
    public GameObject objectToDisable; // Ссылка на объект, который нужно отключить

    public void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Отключаем объект
        }
        else
        {
            Debug.LogWarning("No object to disable!"); // Выводим предупреждение, если ссылка на объект не установлена
        }
    }
}
