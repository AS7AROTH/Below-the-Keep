
using UnityEngine;

// Clase base para las entidades que pueden recibir daño.
public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject deathSoundPrefab;

    private int currentHealth;
    private bool isDead = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Evitamos aplicar daño varias veces después de morir.
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (deathSoundPrefab != null)
        {
            GameObject soundObject = Instantiate(
                deathSoundPrefab,
                transform.position,
                Quaternion.identity
            );

            DeathSoundPlayer player =
                soundObject.GetComponent<DeathSoundPlayer>();

            if (player != null)
            {
                player.PlayDeathSound();
            }
            else
            {
                Destroy(soundObject);
                Debug.LogWarning(
                    "El prefab de muerte no tiene DeathSoundPlayer."
                );
            }
        }

        Destroy(gameObject);
    }
}
