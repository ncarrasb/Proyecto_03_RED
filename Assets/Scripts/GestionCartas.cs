using UnityEngine;

public class GestorCartas : MonoBehaviour
{
    public GameObject[] cartasPrefabs;  // Los prefabs de las 12 cartas (6 cartas repetidas)
    public int numCartas = 12;  // Número total de cartas
    public Vector2 areaMin;  // Esquina inferior izquierda del área
    public Vector2 areaMax;  // Esquina superior derecha del área

    void Start()
    {
        RepartirCartas();
    }

    void RepartirCartas()
    {
        // Crea una lista de posiciones aleatorias dentro del área
        Vector3[] posiciones = new Vector3[numCartas];

        for (int i = 0; i < numCartas; i++)
        {
            // Generar una posición aleatoria dentro del área
            float xPos = Random.Range(areaMin.x, areaMax.x);
            float yPos = Random.Range(areaMin.y, areaMax.y);
            posiciones[i] = new Vector3(xPos, yPos, 0);  // Asignamos la posición en 2D (sin z)

            // Instanciar una carta aleatoriamente seleccionada en esa posición
            int cartaIndex = Random.Range(0, cartasPrefabs.Length);
            Instantiate(cartasPrefabs[cartaIndex], posiciones[i], Quaternion.identity);
        }
    }
}