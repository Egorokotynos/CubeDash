using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    public void Start()
    {
        // �������� ����� DeleteAll() � Start() ��� ������ PlayerPrefs ��� ������� �����.
        PlayerPrefs.DeleteAll();
    }
}
