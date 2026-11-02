// Interfaz que define qué debe tener un objeto que pueda recibir daño.
public interface IDamageable
{
    // Método que deben implementar las clases que puedan recibir daño.
    void TakeDamage(int damage);
}