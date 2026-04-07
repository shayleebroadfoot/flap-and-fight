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

    // void SpawnEnemy()
    // {
    //     int maxAttempts = 15; // Increased attempts for better coverage
    //                           // Added +2.0f offset to spawn X to give horizontal separation from pipes
    //     float spawnX = transform.position.x + 2.0f;
    //     float spawnY;

    //     for (int i = 0; i < maxAttempts; i++)
    //     {
    //         GameObject closestPipe = GetClosestPipe(spawnX);

    //         if (closestPipe != null)
    //         {
    //             float pipeX = closestPipe.transform.position.x;
    //             float distToPipe = Mathf.Abs(pipeX - spawnX);

    //             // If we are horizontally too close to a pipe (less than 1.5 units)
    //             // We restrict the vertical spawn range to be IN the middle of the gap
    //             if (distToPipe < 1.5f)
    //             {
    //                 float gapCenterY = closestPipe.transform.position.y;
    //                 // Constraints: Spawn only within the flyable gap
    //                 spawnY = Random.Range(gapCenterY - 0.15f, gapCenterY + 0.15f);
    //             }
    //             else
    //             {
    //                 // We are far from a pipe, use full vertical range
    //                 spawnY = Random.Range(minY, maxY);
    //             }
    //         }
    //         else
    //         {
    //             spawnY = Random.Range(minY, maxY);
    //         }

    //         Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

    //         // 3. Final safety check: Is this overlapping a pipe object?
    //         if (IsSpawnPositionClear(spawnPos))
    //         {
    //             Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    //             return; // SUCCESS! → stop trying
    //         }
    //     }

    //     // If all attempts fail, don't spawn a bird on this interval.
    // }


    // void SpawnEnemy()
    // {
    //     // 1. Force the bird to spawn horizontally AWAY from the pipe spawner
    //     // If your PipeSpawner is at x=10, spawn birds at x=15
    //     float spawnX = transform.position.x + 2.5f;
    //     float spawnY;

    //     // 2. Use the 'lastPipeY' from your PipeSpawner script to find the gap
    //     // This is the most reliable way since you already save this value!
    //     float gapY = PipeSpawner.lastPipeY;

    //     // 3. We use a 50/50 chance for the bird's position:
    //     if (Random.value > 0.5f)
    //     {
    //         // Option A: Spawn the bird EXACTLY in the middle of the gap
    //         // This is always 'fair' because the player is already heading here
    //         spawnY = gapY;
    //     }
    //     else
    //     {
    //         // Option B: Spawn it slightly above or below the gap center 
    //         // but still within the 'safe' opening
    //         spawnY = gapY + Random.Range(-0.15f, 0.15f);
    //     }

    //     Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

    //     // 4. Final check: if it's still hitting something, don't spawn
    //     if (IsSpawnPositionClear(spawnPos))
    //     {
    //         Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    //     }
    // }

    // private void SpawnPipe()
    // {
    //     float randomY = Random.Range(minY, maxY);
    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);

    //     // --- NEW ENEMY LOGIC ---
    //     // 50% chance to spawn an enemy inside this specific pipe's gap
    //     if (Random.value > 0.5f)
    //     {
    //         // Find the spawn point we just created in the prefab
    //         Transform spawnPoint = pipeN.transform.Find("EnemySpawnPoint");
    //         if (spawnPoint != null)
    //         {
    //             // Spawn the enemy bird at that exact spot
    //             // (Assuming you have a reference to your enemyPrefab here)
    //             // Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, pipeN.transform);
    //         }
    //     }
    //     // -----------------------

    //     Destroy(pipeN, 10f);
    //     lastPipeY = randomY;
    // }


    // bool IsSpawnPositionClear(Vector3 spawnPos)
    // {
    //     // The size of the box we are checking (roughly the size of the enemy bird)
    //     Vector2 checkSize = new Vector2(0.8f, 0.8f);

    //     // Create a layermask for the "Pipes" layer we created.
    //     int pipeLayerMask = LayerMask.GetMask("Pipes");

    //     // OverlapBoxAll only detects objects in the specified layer mask.
    //     Collider2D[] hits = Physics2D.OverlapBoxAll(spawnPos, checkSize, 0f, pipeLayerMask);

    //     // If this array is not empty, it means we hit a pipe. Return false.
    //     if (hits.Length > 0)
    //     {
    //         return false;
    //     }

    //     return true; // We hit nothing in the Pipes layer.
    // }


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
