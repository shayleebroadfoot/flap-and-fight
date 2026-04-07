using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private float velocityBird = 2f;
    [SerializeField] private float rotationSpeed = 10f;

    // Health system variables
    [SerializeField] private int health = 3;
    private float invincibilityTime = 3f;
    private bool isInvincible = false;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //  grab the rigid body in start
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Health:  " + health);
    }

    // Update is called once per frame
    void Update()
    {
        // if we click we will add upwards velocity
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            rb.linearVelocity = Vector2.up * velocityBird;
        }
    }

    private void FixedUpdate()
    {
        //  to control the slight tilt of the bird when it is flapping
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocityY * rotationSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered with: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected");

            if (!isInvincible)
            {
                Debug.Log("Taking damage");
                TakeDamage(1);
            }
            else
            {
                Debug.Log("Ignored due to invincibility");
            }
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health now: " + health);

        // Remove one heart from the UI
        HeartManager heartManager = HeartManager.FindFirstObjectByType<HeartManager>(); // this line is sus: find any object of find first object or find by tag???
        if (heartManager != null)
        {
            heartManager.LoseHeart();
        }

        if (health <= 0)
        {
            GameManager.instance.GameOver();
            return;
        }

        StartCoroutine(InvincibilityCoroutine());
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
    }
}
