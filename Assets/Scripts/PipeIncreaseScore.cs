using UnityEngine;

public class PipeIncreaseScore : MonoBehaviour
{
    // Attach this to the pipe prefab
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Score Triggered with: " + collision.name + " Tag: " + collision.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER PASSED → ADD SCORE");
            Score.instance.UpdateScore();
            // Debug.Log("Triggered!");
        }
    }
}
