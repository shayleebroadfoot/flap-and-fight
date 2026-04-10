using UnityEngine;

public class DivingEnemyMovement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] private float moveSpeed = 0.65f;

    [Header("Dive Behaviour")]
    [SerializeField] private float triggerDistanceX = 2.5f;
    [SerializeField] private float diveSpeedX = 2.2f;
    [SerializeField] private float diveSpeedY = 2.0f;
    [SerializeField] private float yOffsetFromPlayer = 0f;

    private Transform player;
    private bool hasDived = false;
    private float targetY;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (!hasDived)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (player != null)
            {
                float xDistanceToPlayer = transform.position.x - player.position.x;

                if (xDistanceToPlayer <= triggerDistanceX)
                {
                    hasDived = true;
                    targetY = player.position.y + yOffsetFromPlayer;
                }
            }
        }
        else
        {
            Vector3 position = transform.position;

            position.x -= diveSpeedX * Time.deltaTime;

            if (Mathf.Abs(position.y - targetY) > 0.05f)
            {
                float directionY = Mathf.Sign(targetY - position.y);
                position.y += directionY * diveSpeedY * Time.deltaTime;
            }

            transform.position = position;
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}