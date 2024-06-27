using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public GameObject BGMusic1;
    private AudioSource audioSrc1;
    public GameObject[] objs11;

    void Awake()
    {
        objs11 = GameObject.FindGameObjectsWithTag("Sound");
        if (objs11.Length == 0)
        {
            BGMusic1 = Instantiate(BGMusic1);
            BGMusic1.name = "BGMusic1";
            DontDestroyOnLoad(BGMusic1.gameObject);
            Debug.Log("Created new BGMusic1 object.");
        }
        else
        {
            BGMusic1 = GameObject.Find("BGMusic1");
            Debug.Log("Found existing BGMusic1 object.");
        }
    }
    void Start()
    {
        audioSrc1 = BGMusic1.GetComponent<AudioSource>();
    }
}
