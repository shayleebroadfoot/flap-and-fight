using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 2f;

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
        Debug.Log("Bullet hit: " + collision.gameObject.name + " with Tag: " + collision.gameObject.tag);
        // If we hit an enemy, destroy both the enemy and the bullet
        if (collision.CompareTag("Enemy"))
        {
            if (Score.instance != null)
            {
                Score.instance.UpdateScore();
            }


            Destroy(collision.gameObject);
            Destroy(gameObject);
            // You could also add Score.instance.UpdateScore() here!
        }
    }
}