using UnityEngine;

public class PipeSpawner : MonoBehaviour
{ // script names the same as the object

    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.45f;
    [SerializeField] private GameObject pipe;

    // [SerializeField] private GameObject enemyPrefab;


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

    private void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

        GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);

        Transform upper = pipeN.transform.Find("UpperPipe");
        Transform lower = pipeN.transform.Find("LowerPipe");

        Debug.Log("Pipe root spawned at: " + pipeN.transform.position);

        if (upper != null)
        {
            Debug.Log("UpperPipe world pos: " + upper.position);
        }

        if (lower != null)
        {
            Debug.Log("LowerPipe world pos: " + lower.position);
        }

        Destroy(pipeN, 10f);
        lastPipeY = randomY;
    }

    // Original SpawnPipe function without debug logs -- written by Ruth
    // private void SpawnPipe()
    // {
    //     // spawn pipes randomly: position of the spawner +/- random distance (height)
    //     // Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));

    //     // Updated pipe spawning logic
    //     float randomY = Random.Range(minY, maxY);
    //     Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);

    //     // spawn at the location
    //     GameObject pipeN = Instantiate(pipe, spawnPos, Quaternion.identity);

    //     // destroy itself after 10 seconds
    //     Destroy(pipeN, 10f);
    //     lastPipeY = randomY;
    // }
}
