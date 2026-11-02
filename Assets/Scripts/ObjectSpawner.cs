
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnDistance = 5f;
    public float minSpawnDistance = 1.5f;
    public AudioSource spawnAudioSource;

    void Update()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        // Punto fijo delante de la cámara
        Vector3 spawnPosition = cam.transform.position +
                                cam.transform.forward * spawnDistance;

        // Si hay un obstáculo, coloca el cubo antes de alcanzarlo
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, spawnDistance))
        {
            spawnPosition = hit.point - cam.transform.forward * 0.5f;
        }

        // Evita que el cubo aparezca demasiado cerca del jugador
        Vector3 direction = (spawnPosition - transform.position).normalized;

        if (Vector3.Distance(spawnPosition, transform.position) < minSpawnDistance)
        {
            spawnPosition = transform.position + direction * minSpawnDistance;
        }

        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                if (spawnAudioSource != null && spawnAudioSource.clip != null)
                {
                    spawnAudioSource.PlayOneShot(spawnAudioSource.clip);
                }
            }
        }
    }
}
