using UnityEngine;

public class AmbienceZoneTrigger : MonoBehaviour
{
    [Header("Ambientes")]
    [SerializeField] private AudioSource roomAmbience;
    [SerializeField] private AudioSource hallwayAmbience;

    [Header("Música adaptativa")]
    [SerializeField] private AdaptiveMusicController musicController;

    [Header("Audio dinámico")]
    [SerializeField] private float hallwayVolume = 0.7f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Ejecutar los cambios de audio una sola vez al entrar el jugador.
        if (hasTriggered || !other.CompareTag("Player"))
            return;

        hasTriggered = true;

        // Detener el ambiente de la sala.
        if (roomAmbience != null)
            roomAmbience.Stop();

        // Ajustar el volumen del ambiente del pasillo en tiempo de ejecución
        // y reproducirlo al entrar en la zona.
        if (hallwayAmbience != null)
        {
            hallwayAmbience.volume = hallwayVolume;
            hallwayAmbience.Play();
        }

        // Cambiar la música y el snapshot del Audio Mixer al entrar al pasillo.
        if (musicController != null)
            musicController.EntrarAlPasillo();
    }
}