using UnityEngine;
using System.Collections;

public class GestorCartas : MonoBehaviour
{
    public GameObject[] cartasPrefabs;  // Los prefabs de las 12 cartas
    private Carta carta1;  // Primer carta volteada
    private Carta carta2;  // Segunda carta volteada

    private bool carta1Volteada = false;
    private bool carta2Volteada = false;

    // Puntos de los jugadores
    private int puntosJugador1 = 0;
    private int puntosJugador2 = 0;

    void Start()
    {
        RepartirCartas();
    }

    // Reparte las cartas en la escena (esto ya lo tienes configurado)
    void RepartirCartas()
    {
        for (int i = 0; i < cartasPrefabs.Length; i++)
        {
            // Creamos las cartas en posiciones aleatorias
            float xPos = Random.Range(-3f, 3f);
            float yPos = Random.Range(-2f, 2f);
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