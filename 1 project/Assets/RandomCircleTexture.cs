using UnityEngine;

public class RandomCircleTexture : MonoBehaviour
{
    public Sprite[] sprites;  // Масив для збереження 5 спрайтів
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Отримуємо доступ до SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on this object.");
            return;
        }

        // Переконуємось, що є хоча б 5 спрайтів
        if (sprites.Length < 5)
        {
            Debug.LogError("Please assign at least 5 sprites in the inspector.");
            return;
        }

        // Встановлюємо випадковий спрайт
        SetRandomSprite();
    }

    void SetRandomSprite()
    {
        // Генеруємо випадковий індекс для вибору спрайта
        int randomIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[randomIndex];
    }
}
