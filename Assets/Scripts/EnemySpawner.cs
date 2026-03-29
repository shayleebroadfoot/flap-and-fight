using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float minY = 0.5f;
    [SerializeField] float maxY = 2.0f;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
