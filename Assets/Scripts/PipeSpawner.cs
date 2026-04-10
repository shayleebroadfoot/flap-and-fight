using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject pipe;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject staticEnemyPrefab;
    [SerializeField] private GameObject divingEnemyPrefab;

    [Header("Spawn Chances")]
    [SerializeField] private float staticBirdSpawnChance = 0.5f;
    [SerializeField] private float divingBirdSpawnChance = 0.2f;

    [Header("Power Ups")]
    [SerializeField] private GameObject heartPowerUpPrefab;
    [SerializeField] private GameObject shieldPowerUpPrefab;

    [Header("Playable Area")]
    [SerializeField] private float minY = -0.2f;
    [SerializeField] private float maxY = 0.8f;

    [Header("Diving Bird Spawn")]
    [SerializeField] private float divingBirdMinXOffset = 2.5f;
    [SerializeField] private float divingBirdMaxXOffset = 4.0f;
    [SerializeField] private float divingBirdCheckRadius = 0.5f;
    [SerializeField] private int divingBirdSpawnAttempts = 10;

    public static float lastPipeY;
    private float timer;

    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0f;
        }

        timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);

        GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);
        Destroy(pipeN, 10f);

        lastPipeY = randomY;

        // Heart spawn
        if (Random.value < 0.1f)
        {
            Vector3 heartPos = new Vector3(spawnPos.x + 2f, randomY, 0f);
            if (heartPowerUpPrefab != null)
            {
                Instantiate(heartPowerUpPrefab, heartPos, Quaternion.identity);
            }
        }

        // Shield spawn
        if (Random.value < 0.1f)
        {
            Vector3 shieldPos = new Vector3(spawnPos.x + 4f, randomY, 0f);
            if (shieldPowerUpPrefab != null)
            {
                Instantiate(shieldPowerUpPrefab, shieldPos, Quaternion.identity);
            }
        }

        // Static bird: keep your old working logic
        if (Random.value < staticBirdSpawnChance)
        {
            SpawnStaticBird(spawnPos.x, randomY);
        }

        // Diving bird: added separately
        if (Random.value < divingBirdSpawnChance)
        {
            SpawnDivingBird();
        }
    }

    private void SpawnStaticBird(float pipeX, float gapY)
    {
        if (staticEnemyPrefab == null)
        {
            return;
        }

        float randomXOffset = Random.Range(0.8f, 1.5f);
        Vector3 birdPos = new Vector3(pipeX + randomXOffset, gapY, 0f);

        Collider2D hit = Physics2D.OverlapCircle(birdPos, 0.5f);
        if (hit == null)
        {
            GameObject bird = Instantiate(staticEnemyPrefab, birdPos, Quaternion.identity);
            Destroy(bird, 10f);
        }
    }

    private void SpawnDivingBird()
    {
        if (divingEnemyPrefab == null)
        {
            return;
        }

        for (int i = 0; i < divingBirdSpawnAttempts; i++)
        {
            float randomXOffset = Random.Range(divingBirdMinXOffset, divingBirdMaxXOffset);
            float randomY = Random.Range(minY, maxY);

            Vector3 birdPos = new Vector3(transform.position.x + randomXOffset, randomY, 0f);

            Collider2D[] hits = Physics2D.OverlapCircleAll(birdPos, divingBirdCheckRadius);
            bool blocked = false;

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Obstacle") || hit.CompareTag("Enemy"))
                {
                    blocked = true;
                    break;
                }
            }

            if (!blocked)
            {
                GameObject bird = Instantiate(divingEnemyPrefab, birdPos, Quaternion.identity);
                Destroy(bird, 10f);
                return;
            }
        }

        Debug.Log("Diving bird spawn failed.");
    }
}
