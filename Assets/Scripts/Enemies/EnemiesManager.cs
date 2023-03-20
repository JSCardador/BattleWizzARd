using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance { get; private set; }


    [Header("Enemy")]
    [SerializeField] private GameObject _enemy;


    // Private variables
    private float spawnTime = 2f;
    private float timeElapsed = 0f;
    private int spawnRate = 0;


    private List<GameObject> _enemies = new List<GameObject>();

    private Transform _playerTransform;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerTransform = Camera.main.transform;
    }


    /// <summary>
    /// Start spawning enemies
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }


    /// <summary>
    /// Stop spawning enemies
    /// </summary>
    public void PauseGame()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// Call when we win or lose the game.
    /// </summary>
    public void EndGame()
    {
        StopAllCoroutines();
        spawnRate = 0;
        timeElapsed = 0f;
        DeleteAllEnemies();
    }


    /// <summary>
    /// Routine to generate enemies every spawnTime seconds and increase the generation rate every 2 seconds by 1 up to 10 enemies
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            timeElapsed += spawnTime;
            spawnRate = (int)(timeElapsed / 10f); // Increase spawn rate by 1 every  10 seconds
            spawnRate = spawnRate > 10 ? 10 : spawnRate; // Limit spawn rate to 10

            yield return new WaitForSeconds(spawnTime);
            yield return new WaitUntil(() => GameManager.Instance.CurrentGameState == GameManager.GameState.InGame);

            for (int i = 0; i < spawnRate; i++)
            {
                SpawnEnemy(CalculateRandomPosicionToSpawn());
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
        }
    }


    /// <summary>
    ///  Spawn an enemy at the given position
    /// </summary>
    /// <param name="position"></param>
    private void SpawnEnemy(Vector3 position)
    {
        _enemies.Add(Instantiate(_enemy, position, Quaternion.identity));
    }


    private void DeleteAllEnemies()
    {
        foreach (GameObject enemy in _enemies)
        {
            Destroy(enemy);
        }
        _enemies.Clear();
    }


    /// <summary>
    ///  Calculate a random position to spawn an enemy around the player
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateRandomPosicionToSpawn()
    {
        float radius = 5.0f; // Radio del círculo
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return new Vector3(randomCircle.x, FloorDetect.Instance.GetFloorHeight(), randomCircle.y) + _playerTransform.position;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
        // Destroy(enemy);
    }
}
