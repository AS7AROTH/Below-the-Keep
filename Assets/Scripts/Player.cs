using UnityEngine;

// Player hereda de Entity.
// Por eso obtiene el sistema de vida y daño de la clase base.
public class Player : Entity
{
    // Sobrescribimos el método Die de Entity
    // para definir qué sucede específicamente con el jugador.
    protected override void Die()
    {
        // Le avisamos al GameManager que el jugador perdió.
        GameManager.Instance.Defeat();

        // Destruimos el objeto del jugador.
        Destroy(gameObject);
    }
}