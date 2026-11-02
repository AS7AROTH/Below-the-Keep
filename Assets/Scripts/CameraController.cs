using UnityEngine;
using UnityEngine.InputSystem;

// Controla la cámara en tercera persona.
public class CameraController : MonoBehaviour
{
    // Jugador que la cámara debe seguir.
    public Transform target;

    // Distancia de la cámara respecto al jugador.
    public float distance = 7f;

    // Altura de la cámara respecto al jugador.
    public float height = 2f;

    // Sensibilidad del movimiento del mouse.
    public float sensitivity = 0.2f;

    // Distancia mínima para evitar que la cámara se acerque demasiado.
    public float minDistance = 1f;

    // Rotación vertical inicial de la cámara.
    private float rotationX = 20f;

    // Rotación horizontal inicial de la cámara.
    private float rotationY = 0f;

    // LateUpdate actualiza la cámara después del movimiento del jugador.
    void LateUpdate()
    {
        // Si no hay un jugador asignado, no hacemos nada.
        if (target == null)
            return;

        // La cámara responde al mouse mientras la partida está en estado Playing.
        if (Mouse.current != null &&
            GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Obtenemos el movimiento del mouse.
            Vector2 mouse = Mouse.current.delta.ReadValue();

            // Actualizamos la rotación horizontal y vertical.
            rotationY += mouse.x * sensitivity;
            rotationX -= mouse.y * sensitivity;
        }

        // Limitamos la inclinación vertical de la cámara.
        rotationX = Mathf.Clamp(rotationX, -20f, 60f);

        // Creamos la rotación a partir de los valores calculados.
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Punto alrededor del cual gira la cámara.
        Vector3 targetPosition = target.position + Vector3.up * height;

        // Dirección desde el jugador hacia la cámara.
        Vector3 direction = -(rotation * Vector3.forward);

        // Distancia inicial de la cámara.
        float finalDistance = distance;

        // Detectamos obstáculos entre el jugador y la cámara.
        // El Raycast ignora los colliders del jugador.
        RaycastHit hit;
        if (Physics.Raycast(
            targetPosition,
            direction,
            out hit,
            distance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            // Acercamos la cámara al obstáculo, respetando la distancia mínima.
            finalDistance = Mathf.Max(minDistance, hit.distance - 0.2f);
        }

        // Posicionamos la cámara a la distancia calculada.
        transform.position = targetPosition + direction * finalDistance;

        // La cámara mira hacia la parte superior del jugador.
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}