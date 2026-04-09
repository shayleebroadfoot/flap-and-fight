using UnityEngine;

public class PipeSpawner : MonoBehaviour
{ // script names the same as the object

    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject pipe;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float birdSpawnChance = 0.5f;

    [SerializeField] private GameObject heartPowerUpPrefab;

    [SerializeField] private GameObject shieldPowerUpPrefab;


    public static float lastPipeY;
    private float minY = -0.2f;   // above ground
    private float maxY = 0.8f;    // below top

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start by spawning a pipe
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            // once the pipe is destroyes reset timer
            SpawnPipe();
            timer = 0;
        }
        //  increment the timer
        timer += Time.deltaTime;
    }

    // private void SpawnPipe()
    // {
    //     // 1. Calculate Pipe Position
    //     float randomY = Random.Range(minY, maxY);
    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     // 2. Instantiate the Pipe
    //     GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);
    //     Destroy(pipeN, 10f);
    //     lastPipeY = randomY;

    //     // 3. ENEMY SPAWNING LOGIC (Controlled by the Pipe)
    //     // We only spawn an enemy 60% of the time to keep it fair
    //     if (Random.value > 0.4f)
    //     {
    //         // We spawn the bird ahead of the pipe so it's in the empty space
    //         // We use an X offset (e.g., 1.5) to put it BETWEEN pipes
    //         float enemyX = spawnPos.x + 1.5f;

    //         // We use a safe Y range. 
    //         // 50% chance: spawn in the gap. 50% chance: spawn far above/below
    //         float enemyY;
    //         if (Random.value > 0.5f)
    //         {
    //             // Guaranteed safe: Exactly in the pipe gap
    //             enemyY = randomY;
    //         }
    //         else
    //         {
    //             // Pick a height that is far away from the pipe's current Y
    //             // This prevents the "impossible block"
    //             enemyY = (randomY > 0.3f) ? minY : maxY;
    //         }

    //         Vector3 enemySpawnPos = new Vector3(enemyX, enemyY, 0);
    //         Instantiate(enemyPrefab, enemySpawnPos, Quaternion.identity);
    //     }
    // }

    // private void SpawnPipe()
    // {
    //     float randomY = Random.Range(minY, maxY);
    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     // 1. Spawn the Pipe
    //     GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);
    //     Destroy(pipeN, 10f);

    //     float chance = Random.value;

    //     // --- TEST: Increase these to 0.5f just to see if they spawn! ---
    //     if (chance < 0.1f)
    //     {
    //         // Use spawnPos.x so it stays relative to the pipes
    //         Vector3 heartPos = new Vector3(spawnPos.x + 2f, randomY, 0f);
    //         Instantiate(heartPowerUpPrefab, heartPos, Quaternion.identity);
    //     }
    //     else if (chance < 0.2f) // Shield chance
    //     {
    //         Vector3 shieldPos = new Vector3(spawnPos.x + 2f, randomY, 0f);
    //         Instantiate(shieldPowerUpPrefab, shieldPos, Quaternion.identity);
    //     }
    //     else if (chance < birdSpawnChance)
    //     {
    //         float randomXOffset = Random.Range(0.8f, 1.5f);
    //         Vector3 birdPos = new Vector3(spawnPos.x + randomXOffset, randomY, 0);

    //         Collider2D hit = Physics2D.OverlapCircle(birdPos, 0.5f);
    //         if (hit == null)
    //         {
    //             GameObject bird = Instantiate(enemyPrefab, birdPos, Quaternion.identity);
    //             Destroy(bird, 10f);
    //         }
    //     }
    // }

    // force spawn shield instead of pipe.
    private void SpawnPipe()
    {
        // 1. Always Spawn the Pipe
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

        GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);
        Destroy(pipeN, 10f);

        // 2. Heart Spawn (Independent)
        if (Random.value < 0.1f)
        {
            Vector3 heartPos = new Vector3(spawnPos.x + 2f, randomY, 0f);
            if (heartPowerUpPrefab != null) Instantiate(heartPowerUpPrefab, heartPos, Quaternion.identity);
        }

        // 3. Shield Spawn (Independent)
        if (Random.value < 0.1f)
        {
            Vector3 shieldPos = new Vector3(spawnPos.x + 4f, randomY, 0f); // Offset so it doesn't hit the heart
            if (shieldPowerUpPrefab != null) Instantiate(shieldPowerUpPrefab, shieldPos, Quaternion.identity);
        }

        // 4. Bird Spawn (Independent)
        if (Random.value < birdSpawnChance)
        {
            float randomXOffset = Random.Range(0.8f, 1.5f);
            Vector3 birdPos = new Vector3(spawnPos.x + randomXOffset, randomY, 0);

            // Safety check so birds don't stack
            Collider2D hit = Physics2D.OverlapCircle(birdPos, 0.5f);
            if (hit == null)
            {
                GameObject bird = Instantiate(enemyPrefab, birdPos, Quaternion.identity);
                Destroy(bird, 10f);
            }
        }
    }

    // private void SpawnPipe()   // this is working wanted to track sheild
    // {
    //     float randomY = Random.Range(minY, maxY);
    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     // 1. Spawn the Pipe
    //     GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);
    //     Destroy(pipeN, 10f);

    //     float chance = Random.value;

    //     float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x; // new........

    //     if (chance < 0.1f) // 10% chance for a heart power-up
    //     {
    //         // REMOVED "Vector3" from the start of the next line
    //         Vector3 heartPos = new Vector3(playerX + 5f, randomY, 0f);
    //         // Vector3 heartPos = new Vector3(transform.position.x + 11.0f, randomY, 0f);
    //         // Vector3 heartPos = new Vector3(spawnPos.x + 5.0f, randomY, 0f);
    //         // Vector3 heartPos = new Vector3(spawnPos.x + 1.2f, randomY, 0f);
    //         Instantiate(heartPowerUpPrefab, heartPos, Quaternion.identity);
    //     }
    //     if (Random.value < 0.1f) // 10% chance
    //     {
    //         Vector3 shieldPos = new Vector3(spawnPos.x + 1.5f, randomY, 0f);
    //         Instantiate(shieldPowerUpPrefab, shieldPos, Quaternion.identity);
    //     }
    //     else if (chance < birdSpawnChance) // Spawn bird if heart didn't spawn
    //     {
    //         // REMOVED the extra 'if (Random.value < birdSpawnChance)' because 
    //         // it was already checked in the 'else if' above.

    //         float randomXOffset = Random.Range(0.8f, 1.5f);
    //         Vector3 birdPos = new Vector3(spawnPos.x + randomXOffset, randomY, 0);

    //         // Check if the spot is clear
    //         Collider2D hit = Physics2D.OverlapCircle(birdPos, 0.5f);

    //         if (hit == null)
    //         {
    //             GameObject bird = Instantiate(enemyPrefab, birdPos, Quaternion.identity);
    //             Destroy(bird, 10f);
    //         }
    //         else
    //         {
    //             Debug.Log("Spawn blocked: Another bird was too close!");
    //         }
    //     }
    // }


}
