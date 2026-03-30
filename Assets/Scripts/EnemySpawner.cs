using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject pipePrefab;

    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float minY = -0.2f;
    [SerializeField] private float maxY = 0.8f;
    [SerializeField] private float gapOffset = 0.0f;

    private float enemyHalfWidth;
    private float pipeHalfWidth;
    private float pipeCheckDistance;
    private float timer;

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

    // void SpawnEnemy()
    // {
    //     float spawnX = transform.position.x;
    //     float spawnY;

    //     GameObject closestPipe = GetClosestPipe(spawnX);

    //     if (closestPipe != null)
    //     {
    //         float xDistance = Mathf.Abs(closestPipe.transform.position.x - spawnX);

    //         Debug.Log(
    //             "spawnX: " + spawnX +
    //             " | closestPipe: " + closestPipe.name +
    //             " | pipeX: " + closestPipe.transform.position.x +
    //             " | pipeY: " + closestPipe.transform.position.y +
    //             " | xDistance: " + xDistance +
    //             " | pipeCheckDistance: " + pipeCheckDistance
    //         );

    //         if (xDistance < pipeCheckDistance)
    //         {
    //             float gapCenterY = closestPipe.transform.position.y;
    //             spawnY = gapCenterY + Random.Range(-gapOffset, gapOffset);
    //             Debug.Log("Using GAP spawn");
    //         }
    //         else
    //         {
    //             if (Random.value > 0.5f)
    //             {
    //                 spawnY = Random.Range(0.4f, maxY);
    //             }
    //             else
    //             {
    //                 spawnY = Random.Range(minY, 0.1f);
    //             }
    //             Debug.Log("Using FREE spawn");
    //         }
    //     }
    //     else
    //     {
    //         if (Random.value > 0.5f)
    //         {
    //             spawnY = Random.Range(0.4f, maxY);
    //         }
    //         else
    //         {
    //             spawnY = Random.Range(minY, 0.1f);
    //         }
    //         Debug.Log("Using FREE spawn - no pipe found");
    //     }

    //     Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

    //     if (IsSpawnPositionClear(spawnPos))
    //     {
    //         Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    //         Debug.Log("Enemy spawned at: " + spawnPos);
    //     }
    //     else
    //     {
    //         Debug.Log("Blocked enemy spawn at: " + spawnPos);
    //     }
    // }

    void SpawnEnemy()
    {
        float spawnX = transform.position.x;
        float spawnY;

        GameObject closestPipe = GetClosestPipe(spawnX);

        if (closestPipe != null && Mathf.Abs(closestPipe.transform.position.x - spawnX) < pipeCheckDistance)
        {
            float gapCenterY = closestPipe.transform.position.y;
            spawnY = gapCenterY + Random.Range(-gapOffset, gapOffset);
        }
        else
        {
            if (Random.value > 0.5f)
            {
                spawnY = Random.Range(0.4f, maxY);
            }
            else
            {
                spawnY = Random.Range(minY, 0.1f);
            }
        }

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        if (IsSpawnPositionClear(spawnPos))
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Enemy spawned at: " + spawnPos);
        }
        else
        {
            Debug.Log("Blocked enemy spawn at: " + spawnPos);
        }
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
            new Vector2(checkHalfWidth * 2f, checkHalfHeight * 2f),
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
