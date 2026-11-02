using UnityEngine;

// Controla la zona que permite ganar la partida.
public class Goal : MonoBehaviour
{
    // Se ejecuta cuando otro objeto entra
    // en el Collider configurado como Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que entró es el jugador.
        if (other.CompareTag("Player"))
        {
            // Avisamos al GameManager que el jugador ganó.
            GameManager.Instance.Victory();
        }
    }
}