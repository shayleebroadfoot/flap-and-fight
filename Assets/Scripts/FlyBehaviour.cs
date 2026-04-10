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

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform shootingPoint; // Where the bullet spawns (the bird's beak)

    [SerializeField] private GameObject shieldVisual;
    [SerializeField] private float shieldDuration = 5f;

    private bool hasShield = false;


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
        // Debug.Log("Player position: " + transform.position);
        // if we click we will add upwards velocity
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            rb.linearVelocity = Vector2.up * velocityBird;
        }

        // this is for shooting enemies
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }

        if (hasShield && shieldVisual != null)
        {
            float scale = 1.5f + Mathf.Sin(Time.time * 5f) * 0.1f;
            shieldVisual.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    private void FixedUpdate()
    {
        //  to control the slight tilt of the bird when it is flapping
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocityY * rotationSpeed);
    }

    void Shoot()
    {
        // Create the bullet at the shooting point's position
        Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayShootSfx();
        }
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

        if (collision.CompareTag("ShieldPowerUp"))
        {
            ActivateShield();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected");

            if (hasShield)
            {
                Debug.Log("Shield absorbed damage!");
                Destroy(collision.gameObject);
                return;
            }

            if (!isInvincible)
            {
                Debug.Log("Taking damage");

                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlayEnemyHitSfx();
                }

                TakeDamage(1);
            }
        }

        if (collision.CompareTag("HeartPowerUp"))
        {
            HeartManager heartManager = Object.FindFirstObjectByType<HeartManager>();

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayHeartPickupSfx();
            }

            if (health < 3)
            {
                if (heartManager != null)
                {
                    heartManager.AddHeart();
                }

                health++;
                Debug.Log("Healed! Health is now: " + health);
            }
            else
            {
                if (Score.instance != null)
                {
                    Score.instance.UpdateScore();
                    Debug.Log("Full health! +1 Point added instead.");
                }
            }

            Destroy(collision.gameObject);
        }
    }
    void TakeDamage(int damage)
    {

        if (hasShield) return;
        health -= damage;
        Debug.Log("Health now: " + health);

        // Remove one heart from the UI
        HeartManager heartManager = HeartManager.FindFirstObjectByType<HeartManager>(); // this line is ??: find any object of find first object or find by tag???
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

    public void ActivateShield()
    {
        if (hasShield) return;

        hasShield = true;
        shieldVisual.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayPowerUpMusic();
        }

        StartCoroutine(ShieldCoroutine());
    }

    IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(shieldDuration);

        hasShield = false;
        shieldVisual.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayNormalMusic();
        }
    }
}
