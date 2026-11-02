using UnityEngine;
using UnityEngine.SceneManagement;

// Controla el estado general de la partida,
// incluyendo menú, victoria y derrota.
public class GameManager : MonoBehaviour
{
    // Instancia única del GameManager.
    public static GameManager Instance;

    // Referencia al panel inicial.
    [SerializeField] private GameObject startPanel;

    // Referencia al panel de victoria.
    [SerializeField] private GameObject victoryPanel;

    // Referencia al panel de derrota.
    [SerializeField] private GameObject defeatPanel;

    // Estados posibles de la partida.
    public enum GameState
    {
        Menu,
        Playing,
        Victory,
        Defeat
    }

    // Estado actual de la partida.
    public GameState currentState;

    // Awake se ejecuta cuando se crea el objeto.
    private void Awake()
    {
        // Si todavía no existe una instancia...
        if (Instance == null)
        {
            // ...esta pasa a ser la instancia principal.
            Instance = this;
        }
        else
        {
            // Si ya existe otra instancia,
            // destruimos esta para evitar duplicados.
            Destroy(gameObject);
        }
    }

    // Se ejecuta al comenzar la escena.
    private void Start()
    {
        // Comenzamos mostrando el menú.
        currentState = GameState.Menu;

        // Activamos el panel inicial.
        startPanel.SetActive(true);

        // Ocultamos los paneles de victoria y derrota.
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);

        // Mostramos y liberamos el cursor para el menú.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Comienza la partida.
    public void StartGame()
    {
        // Cambiamos el estado a Playing.
        currentState = GameState.Playing;

        // Ocultamos el menú.
        startPanel.SetActive(false);

        // Ocultamos y bloqueamos el cursor.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Se llama cuando el jugador gana.
    public void Victory()
    {
        // Cambiamos el estado a Victory.
        currentState = GameState.Victory;

        // Mostramos el panel de victoria.
        victoryPanel.SetActive(true);

        // Mostramos y liberamos el cursor.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Mensaje de prueba en la consola.
        Debug.Log("¡VICTORIA!");
    }

    // Se llama cuando el jugador pierde.
    public void Defeat()
    {
        // Cambiamos el estado a Defeat.
        currentState = GameState.Defeat;

        // Mostramos el panel de derrota.
        defeatPanel.SetActive(true);

        // Mostramos y liberamos el cursor.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Mensaje de prueba en la consola.
        Debug.Log("DERROTA");
    }

    // Reinicia la escena actual.
    public void RestartGame()
    {
        // Volvemos a cargar la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}