using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Update is called once per frame
    public float speed = 0.65f;
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        // destroy when off screen (same idea as pipes)
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
