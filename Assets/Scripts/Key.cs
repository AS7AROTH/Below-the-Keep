
using UnityEngine;

// Controla el comportamiento de la llave.
public class Key : MonoBehaviour
{
    [SerializeField] private GameObject pickupSoundPrefab;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        PlayerMovement player =
            other.GetComponentInParent<PlayerMovement>();

        if (player != null)
        {
            collected = true;

            player.hasKey = true;

            Debug.Log("Llave recogida");

            // Reproducimos el sonido desde un objeto independiente.
            if (pickupSoundPrefab != null)
            {
                GameObject soundObject = Instantiate(
                    pickupSoundPrefab,
                    transform.position,
                    Quaternion.identity
                );

                SoundEffectPlayer soundPlayer =
                    soundObject.GetComponent<SoundEffectPlayer>();

                if (soundPlayer != null)
                {
                    soundPlayer.PlaySound();
                }
                else
                {
                    Destroy(soundObject);
                    Debug.LogWarning(
                        "El prefab de sonido no tiene SoundEffectPlayer."
                    );
                }
            }

            Destroy(gameObject);
        }
    }
}
