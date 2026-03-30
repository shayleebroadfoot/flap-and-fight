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
    [SerializeField] private float invincibilityTime = 1f;

    private bool isInvincible = false;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //  grab the rigid body in start
        rb = GetComponent<Rigidbody2D>();
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

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Collided with: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);
    //     // instant death: pipes or ground
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         GameManager.instance.GameOver();
    //         return;
    //     }

    //     // enemy damage
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         if (!isInvincible)
    //         {
    //             TakeDamage(1);
    //         }
    //     }
    // }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        // enemy -> damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                TakeDamage(1);
            }
            return;
        }

        // everything else -> instant death (pipes, ground, etc.)
        GameManager.instance.GameOver();
    }

    void TakeDamage(int damage)
    {
        health -= damage;

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
