using UnityEngine;

public class Carta : MonoBehaviour
{
    public string valor; // para asignarle un valor a cada carta 
    public Sprite frontSprite; 
    public Sprite backSprite;   

    private SpriteRenderer spriteRenderer;  

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backSprite; 
    }

    // Método para voltear la carta
    public void VoltearCarta()
    {
        if (spriteRenderer.sprite == backSprite)
            spriteRenderer.sprite = frontSprite;  
        else
            spriteRenderer.sprite = backSprite;  
    }
}