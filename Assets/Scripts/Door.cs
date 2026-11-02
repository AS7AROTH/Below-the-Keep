
using UnityEngine;

// Controla el comportamiento de la puerta.
public class Door : MonoBehaviour
{
    public bool isOpen;
    public PlayerMovement player;

    [Header("Sonido de apertura")]
    [SerializeField] private GameObject doorOpenSoundPrefab;

    // Abre la puerta si el jugador tiene la llave.
    public void Open()
    {
        // No hacemos nada si la puerta ya está abierta.
        if (isOpen)
            return;

        // Verificamos que la referencia al jugador exista.
        if (player == null)
            return;

        Debug.Log("Intentando abrir puerta. Tiene llave: " + player.hasKey);

        // Solo permitimos abrir la puerta si tiene la llave.
        if (player.hasKey)
        {
            isOpen = true;

            // Reproducimos el sonido desde un objeto independiente.
            if (doorOpenSoundPrefab != null)
            {
                GameObject soundObject = Instantiate(
                    doorOpenSoundPrefab,
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
                        "El prefab de apertura no tiene SoundEffectPlayer."
                    );
                }
            }

            // Rotamos la puerta para simular que se abre.
            transform.Rotate(0, 90, 0);

            Debug.Log("Puerta abierta");
        }
        else
        {
            Debug.Log("No tenés la llave");
        }
    }
}