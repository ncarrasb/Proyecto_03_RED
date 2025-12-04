using UnityEngine;
using System.Collections;

public class GestorCartas : MonoBehaviour
{
    public GameObject[] cartasPrefabs;  // Los prefabs de las 12 cartas
    private Carta carta1;  // Primer carta volteada
    private Carta carta2;  // Segunda carta volteada

    private bool carta1Volteada = false;
    private bool carta2Volteada = false;

    void Start()
    {
        RepartirCartas();
    }

    // Reparte las cartas en la escena (esto ya lo tienes configurado)
    void RepartirCartas()
    {
        int cartasPorFila = 4;
        int filas = 3;

        for (int i = 0; i < cartasPrefabs.Length; i++)
        {
            // Calculamos la fila y la columna para cada carta
            int fila = i / cartasPorFila;
            int columna = i % cartasPorFila;

            // Calculamos la posición para la carta
            float xPos = -3 + columna * 2;  // Ajusta la distancia entre las cartas
            float yPos = 2 - fila * 2;

            // Creamos una carta en la posición calculada
            Instantiate(cartasPrefabs[i], new Vector3(xPos, yPos, 0), Quaternion.identity);
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