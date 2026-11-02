using UnityEngine;

// Enemy hereda de Entity.
// Por eso también tiene el sistema de vida y daño.
public class Enemy : Entity
{
    // Referencia al jugador que el enemigo va a perseguir.
    private Transform player;

    // Referencia al Rigidbody del enemigo para moverlo con física.
    private Rigidbody rb;

    // Velocidad de movimiento del enemigo.
    [SerializeField] private float speed = 2f;

    // Start se ejecuta al comenzar la partida.
    private void Start()
    {
        // Busca en la escena al objeto que tenga el Tag "Player".
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Obtiene el Rigidbody del propio enemigo.
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate se utiliza para trabajar con movimiento basado en física.
    private void FixedUpdate()
    {
        // Si no existe el jugador, no hacemos nada.
        if (player == null)
        {
            return;
        }

        // Calculamos la dirección desde el enemigo hacia el jugador.
        Vector3 direction = player.position - transform.position;

        // Ignoramos la diferencia de altura.
        // De esta forma el enemigo no intenta volar hacia el jugador.
        direction.y = 0;

        // Comprobamos que haya una distancia suficiente para moverse.
        if (direction.magnitude > 0.1f)
        {
            // Movemos el Rigidbody en dirección al jugador.
            rb.MovePosition(
                rb.position + direction.normalized * speed * Time.fixedDeltaTime
            );
        }
    }

    // Se ejecuta cuando el enemigo colisiona con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto con el que chocamos es el jugador.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscamos el componente Player en el objeto.
            Player player = collision.gameObject.GetComponent<Player>();

            // Comprobamos que el componente exista.
            if (player != null)
            {
                // El enemigo le hace 1 punto de daño al jugador.
                player.TakeDamage(1);

                Debug.Log("El enemigo hizo daño al jugador");
            }
        }
    }
}