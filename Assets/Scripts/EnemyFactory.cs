using UnityEngine;

// Se encarga de crear los enemigos.
public class EnemyFactory : MonoBehaviour
{
    // Prefab que vamos a utilizar para crear los enemigos.
    [SerializeField] private GameObject enemyPrefab;

    // Lugares donde queremos que aparezcan los enemigos.
    [SerializeField] private Transform[] spawnPoints;

    // Se ejecuta una vez al comenzar la escena.
    private void Start()
    {
        // Recorremos todos los puntos de aparición.
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Creamos un enemigo en cada punto.
            CreateEnemy(spawnPoint.position);
        }
    }

    // Crea un enemigo en la posición indicada.
    public Enemy CreateEnemy(Vector3 position)
    {
        // Creamos una copia del prefab
        // en la posición recibida.
        GameObject enemyObject = Instantiate(
            enemyPrefab,
            position,
            Quaternion.identity
        );

        // Devolvemos el componente Enemy
        // del enemigo recién creado.
        return enemyObject.GetComponent<Enemy>();
    }
}