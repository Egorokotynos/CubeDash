using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    public void Start()
    {
        // Вызываем метод DeleteAll() в Start() для сброса PlayerPrefs при запуске сцены.
        PlayerPrefs.DeleteAll();
    }
}
