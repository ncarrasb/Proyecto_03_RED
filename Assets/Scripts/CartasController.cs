using UnityEngine;

public class Carta : MonoBehaviour
{
    public string valor; // para asignarle un valor a cada carta 
    public Sprite frontSprite; 
    public Sprite backSprite;   

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private bool estaGirada = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = backSprite; 
    }


    void Update()
    {
        // Detecta el clic del jugador sobre la carta
        if (Input.GetMouseButtonDown(0))  // Clic izquierdo del ratón
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider.OverlapPoint(mousePos))  // Comprobamos si el clic fue sobre la carta
            {
                GirarCarta();  // Volteamos la carta si ha sido clickeada
            }
        }
    }



    // Método para girar la carta
    public void GirarCarta()
    {
        if (!estaGirada)
        {
            spriteRenderer.sprite = frontSprite;  // Muestra la parte frontal de la carta
            estaGirada = true;

            // Llama a la función para comprobar si la carta forma una pareja
            GestorCartas gestorCartas = FindObjectOfType<GestorCartas>();  // Encuentra el script GestorCartas en la escena
            gestorCartas.ComprobarPareja(this);  // Pasa esta carta como parámetro
        }
        else
        {
            spriteRenderer.sprite = backSprite;  // Vuelve a la parte trasera de la carta
            estaGirada = false;
        }
    }

}