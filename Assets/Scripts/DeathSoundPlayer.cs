
using UnityEngine;

public class DeathSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Destroy(gameObject);
            return;
        }

        // Elegimos entre el sonido original y una variante más grave.
        audioSource.pitch = Random.value < 0.5f ? 1f : 0.9f;

        audioSource.Play();

        // El objeto vive hasta que termina el sonido.
        Destroy(gameObject, audioSource.clip.length / audioSource.pitch);
    }
}
