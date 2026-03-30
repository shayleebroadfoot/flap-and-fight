using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] float spawnInterval = 3f;
    float minY = -0.2f;
    float maxY = 0.8f;
    private float pipeCheckDistance = 1.7f; // horizontal overlap range
    private float gapOffset = 0.5f; // how tight inside gap
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // if (Random.value > 0.6f)
            // {
            //     SpawnEnemy();
            // }
            SpawnEnemy();
            timer = 0f;
        } 
    }
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

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Enemy spawned at: " + spawnPos);
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
}
