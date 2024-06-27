using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class FakeLoad : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine that will change the scene after 3 seconds
        StartCoroutine(ChangeSceneAfterDelay(3f));
    }

    // Coroutine to change the scene after a delay
    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
