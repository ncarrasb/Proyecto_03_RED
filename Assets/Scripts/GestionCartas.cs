using UnityEngine;
using System.Collections;

public class GestorCartas : MonoBehaviour
{
    public GameObject[] cartasPrefabs;  // Los prefabs de las 12 cartas
    public int numCartas = 12;
    public float distanciaEntreCartas = 3f;
    public Vector2 inicioPosicion;
    private Carta carta1;  // Primer carta volteada
    private Carta carta2;  // Segunda carta volteada

    private bool carta1Volteada = false;
    private bool carta2Volteada = false;

    // Puntos de los jugadores
    private int puntosJugador1 = 0;
    private int puntosJugador2 = 0;

    void Start()
    {
        CalcularInicioPosicion();
        RepartirCartas();
    }

    // Calcular el punto de inicio para centrar las cartas
    void CalcularInicioPosicion()
    {
        int cartasPorFila = 4;  // Número de cartas por fila
        int filas = 3;  // Número de filas

        // Calcular el ancho y la altura total de la cuadrícula de cartas
        float anchoTotal = (cartasPorFila - 1) * distanciaEntreCartas;
        float alturaTotal = (filas - 1) * distanciaEntreCartas;

        // Calcular la posición de inicio para centrar las cartas
        // El centro de la escena será el centro de la cuadrícula
        inicioPosicion = new Vector2(-anchoTotal / 2, alturaTotal / 2);
    }

    // Reparte las cartas de manera aleatoria en las posiciones fijas
    void RepartirCartas()
    {
        int cartasPorFila = 4;  // Número de cartas por fila
        int filas = 3;  // Número de filas

        for (int i = 0; i < numCartas; i++)
        {
            // Calculamos la fila y la columna para cada carta
            int fila = i / cartasPorFila;  // Determina la fila
            int columna = i % cartasPorFila;  // Determina la columna

            // Calculamos la posición para la carta
            float xPos = inicioPosicion.x + columna * distanciaEntreCartas;
            float yPos = inicioPosicion.y - fila * distanciaEntreCartas;

            // Creamos una carta en la posición calculada
            int cartaIndex = Random.Range(0, cartasPrefabs.Length);
            Instantiate(cartasPrefabs[cartaIndex], new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }

    // Llamado cuando el jugador voltea una carta
    public void ComprobarPareja(Carta cartaVolteada)
    {
        if (!carta1Volteada)
        {
            carta1 = cartaVolteada;
            carta1Volteada = true;
        }
        else if (!carta2Volteada)
        {
            carta2 = cartaVolteada;
            carta2Volteada = true;

            // Compara las dos cartas volteadas
            if (carta1.valor == carta2.valor)
            {
                // Las cartas son una pareja, dejaras de voltear o las marcarás como emparejadas
                Debug.Log("¡Pareja encontrada!");
            }
            else
            {
                // Si no son una pareja, espera un poco y luego las volteas de nuevo
                StartCoroutine(VoltearCartasDeNuevo());
            }
        }
    }

    // Espera un tiempo antes de voltear las cartas si no son una pareja
    IEnumerator VoltearCartasDeNuevo()
    {
        yield return new WaitForSeconds(1f);  // Espera 1 segundo

        carta1.GirarCarta();  // Vuelve a voltear la primera carta
        carta2.GirarCarta();  // Vuelve a voltear la segunda carta

        carta1Volteada = false;  // Resetea el estado
        carta2Volteada = false;  // Resetea el estado
    }
}