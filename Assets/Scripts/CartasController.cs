using UnityEngine;
using Unity.Netcode;

public class Carta : NetworkBehaviour
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
        // Si esta carta no ha sido girada aún, la giramos
        if (!estaGirada)
        {
            // Llamamos al RPC para que todos los clientes vean el giro
            GirarCartaServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    // Método ServerRPC para girar la carta en todos los clientes
    [ServerRpc(RequireOwnership = false)]
    public void GirarCartaServerRpc(ulong clientId)
    {
        // Verificar si el jugador es el propietario de la carta (usando NetworkObject)
        if (NetworkObject.IsOwner)
        {
            // Realiza el giro
            if (!estaGirada)
            {
                spriteRenderer.sprite = frontSprite;  // Muestra el frente de la carta
                estaGirada = true;
            }
            else
            {
                spriteRenderer.sprite = backSprite;  // Muestra la parte trasera de la carta
                estaGirada = false;
            }

            // Aquí podrías agregar más lógica para la comprobación de pareja, si lo necesitas
            GestorCartas gestorCartas = FindObjectOfType<GestorCartas>();
            gestorCartas.ComprobarPareja(this);
        }
    }
}