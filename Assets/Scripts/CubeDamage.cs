
using System.Collections.Generic;
using UnityEngine;

// Controla el daño y el sonido de impacto del cubo.
public class CubeDamage : MonoBehaviour
{
    private bool canDamage = true;

    private List<Enemy> enemiesDamaged = new List<Enemy>();

    [Header("Sonido de impacto")]
    [SerializeField] private AudioSource impactAudioSource;

    private void Start()
    {
        Invoke(nameof(StopDamage), 1f);
    }

    private void StopDamage()
    {
        canDamage = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canDamage)
            return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null && !enemiesDamaged.Contains(enemy))
        {
            // Reproducimos el sonido cuando el cubo golpea al enemigo.
            if (impactAudioSource != null &&
                impactAudioSource.clip != null)
            {
                impactAudioSource.PlayOneShot(
                    impactAudioSource.clip
                );
            }

            // Aplicamos el daño original.
            enemy.TakeDamage(1);

            enemiesDamaged.Add(enemy);

            Debug.Log("El cubo hizo daño a " + enemy.gameObject.name);
        }
    }
}
