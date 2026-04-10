using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private GameObject enemyHitParticlesPrefab;

    void Start()
    {
        // Destroy the bullet after a few seconds so they don't clutter the game
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the bullet to the right
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet trigger hit: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        if (collision.CompareTag("Enemy"))
        {
            if (Score.instance != null)
            {
                Score.instance.UpdateScore();
            }

            if (enemyHitParticlesPrefab != null)
            {
                Instantiate(enemyHitParticlesPrefab, collision.transform.position, Quaternion.identity);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collision hit: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}