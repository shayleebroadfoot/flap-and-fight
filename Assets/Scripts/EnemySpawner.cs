using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float minY = -0.2f;
    [SerializeField] float maxY = 0.8f;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            if (Random.value > 0.6f)
            {
                SpawnEnemy();
            }
            timer = 0f;
        } 
    }

    void SpawnEnemy()
    {
        // pick top or bottom region intentionally
        float spawnY;

        if (Random.value > 0.5f)
        {
            // spawn near top
            spawnY = Random.Range(0.4f, maxY);
        }
        else
        {
            // spawn near bottom
            spawnY = Random.Range(minY, 0.1f);
        }

        Vector3 spawnPos = new Vector3(transform.position.x, spawnY, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // void SpawnEnemy()
    // {
    //     float randomY = Random.Range(minY, maxY);

    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    // }
}
