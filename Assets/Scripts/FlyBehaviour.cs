using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private float velocityBird = 2f;
    [SerializeField] private float rotationSpeed = 10f;


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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // whenever our player collides, call game over
        GameManager.instance.GameOver();



        // future work -------------- hit by enemy or enemy attacks

    }
}
