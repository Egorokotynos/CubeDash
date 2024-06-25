using UnityEngine;

public class RandomCircleTexture : MonoBehaviour
{
    public Sprite[] sprites;  // ����� ��� ���������� 5 �������
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // �������� ������ �� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on this object.");
            return;
        }

        // ������������, �� � ���� � 5 �������
        if (sprites.Length < 5)
        {
            Debug.LogError("Please assign at least 5 sprites in the inspector.");
            return;
        }

        // ������������ ���������� ������
        SetRandomSprite();
    }

    void SetRandomSprite()
    {
        // �������� ���������� ������ ��� ������ �������
        int randomIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randomIndex];
    }
}
