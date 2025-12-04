using UnityEngine;

public class GestorCartas : MonoBehaviour
{
    public GameObject[] cartasPrefabs;  
    public int numCartas = 12;  // Número total de cartas
    public float distanciaEntreCartas = 2f;  // Distancia entre cartas
    public Vector2 inicioPosicion = new Vector2(-5, 5);  // Punto de inicio para colocar las cartas (esquina superior izquierda)

    void Start()
    {
        RepartirCartas();
    }

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
}