using UnityEngine;

public class DisableObjectOnButton : MonoBehaviour
{
    public GameObject objectToDisable; // ������ �� ������, ������� ����� ���������

    public void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // ��������� ������
        }
        else
        {
            Debug.LogWarning("No object to disable!"); // ������� ��������������, ���� ������ �� ������ �� �����������
        }
    }
}
