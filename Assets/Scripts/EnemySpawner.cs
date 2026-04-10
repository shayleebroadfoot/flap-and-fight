using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject pipePrefab;

    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float minY = -0.2f;
    [SerializeField] private float maxY = 0.8f;
    [SerializeField] private float gapOffset = 0.6f; // changed from 0.0f -> 0.06f 

    private float enemyHalfWidth;
    private float pipeHalfWidth;
    private float pipeCheckDistance;
    private float timer;

    // private float safeMargin = 0.2f; // safe margin to spawn enemy

    void Start()
    {
        GameObject tempEnemy = Instantiate(enemyPrefab, new Vector3(1000f, 1000f, 0f), Quaternion.identity);
        GameObject tempPipe = Instantiate(pipePrefab, new Vector3(1000f, 1000f, 0f), Quaternion.identity);

        Collider2D enemyCol = tempEnemy.GetComponentInChildren<Collider2D>(true);
        Collider2D pipeCol = tempPipe.GetComponentInChildren<Collider2D>(true);

        if (enemyCol != null)
        {
            enemyHalfWidth = enemyCol.bounds.extents.x;
        }

        if (pipeCol != null)
        {
            pipeHalfWidth = pipeCol.bounds.extents.x;
        }

        pipeCheckDistance = enemyHalfWidth + pipeHalfWidth;

        Debug.Log("enemyHalfWidth: " + enemyHalfWidth +
                " | pipeHalfWidth: " + pipeHalfWidth +
                " | pipeCheckDistance: " + pipeCheckDistance);

        Destroy(tempEnemy);
        Destroy(tempPipe);
    }

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
        int maxAttempts = 5;

        for (int i = 0; i < maxAttempts; i++)
        {
            float spawnX = transform.position.x;
            float spawnY;

            GameObject closestPipe = GetClosestPipe(spawnX);

            if (closestPipe != null && Mathf.Abs(closestPipe.transform.position.x - spawnX) < pipeCheckDistance)
            {
                float gapCenterY = closestPipe.transform.position.y;

                float safeMargin = 0.2f;

                // spawnY = gapCenterY + Random.Range(  // birds were spawning in between the pipe opening (NOT GOOD)
                //     -gapOffset + safeMargin,
                //      gapOffset - safeMargin
                // );

                // randomly choose top or bottom
                if (Random.value > 0.5f)
                {
                    // spawn ABOVE the gap
                    spawnY = Random.Range(gapCenterY + gapOffset + safeMargin, maxY);
                }
                else
                {
                    // spawn BELOW the gap
                    spawnY = Random.Range(minY, gapCenterY - gapOffset - safeMargin);
                }
            }
            else
            {
                spawnY = Random.Range(minY, maxY);
            }

            Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

            if (IsSpawnPositionClear(spawnPos))
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                Debug.Log("Enemy spawned at: " + spawnPos);
                return; // SUCCESS → stop trying
            }
        }

        // If all attempts fail:
        Debug.Log("Failed to find valid spawn position");
    }

    GameObject GetClosestPipe(float xPos)
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject pipe in pipes)
        {
            float dist = Mathf.Abs(pipe.transform.position.x - xPos);

            if (dist < minDistance)
            {
                minDistance = dist;
                closest = pipe;
            }
        }

        return closest;
    }

    bool IsSpawnPositionClear(Vector3 spawnPos)
    {
        float checkHalfWidth = enemyHalfWidth * 2f;
        float checkHalfHeight = 0.19f * 2f;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            spawnPos,
            // new Vector2(checkHalfWidth * 2f, checkHalfHeight * 2f), // doubled already, we are overchecking
            new Vector2(checkHalfWidth, checkHalfHeight),
            0f
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Obstacle"))
            {
                return false;
            }
        }

        return true;
    }
}
