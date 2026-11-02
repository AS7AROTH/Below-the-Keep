using UnityEngine;
using UnityEngine.InputSystem;

// Permite al jugador interactuar con objetos del escenario.
public class PlayerInteraction : MonoBehaviour
{
    // Se ejecuta en cada frame.
    void Update()
    {
        // Comprobamos si el jugador presionó la tecla E.
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Creamos un Raycast desde el centro de la cámara.
            Ray ray = Camera.main.ViewportPointToRay(
                new Vector3(0.5f, 0.5f, 0)
            );

            // Buscamos todos los objetos que el Raycast encuentre.
            RaycastHit[] hits = Physics.RaycastAll(ray);

            // Recorremos todos los objetos detectados.
            foreach (RaycastHit hit in hits)
            {
                // Buscamos un componente Door en el objeto
                // o en alguno de sus padres.
                Door door = hit.collider.GetComponentInParent<Door>();

                // Si encontramos una puerta...
                if (door != null)
                {
                    // ...intentamos abrirla.
                    door.Open();

                    // Terminamos la búsqueda.
                    break;
                }
            }
        }
    }
}