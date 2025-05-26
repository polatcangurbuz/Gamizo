using System.Collections;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float spawnInterval = 5f;

    public static enemySpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Time.time >= spawnInterval)
        {
            SpawnEnemy();
            spawnInterval = 5f + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.transform.rotation);
        StartCoroutine(MoveEnemy(enemy));
    }

    private IEnumerator MoveEnemy(GameObject enemy)
    {
        while (enemy != null)
        {
            if (Vector3.Distance(enemy.transform.position, target.position) <= 0.3f)
            {
                ballSpawner.Instance?.FireBall(enemy, target);
                yield return new WaitForSeconds(1f);
                if (enemy != null) Destroy(enemy);
                yield break;
            }

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
    }
}