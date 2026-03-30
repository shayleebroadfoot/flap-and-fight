using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float speedPipe = 0.65f;

    // Update is called once per frame
    void Update()
    {
        // move the pipes left every single frame 
        transform.position += Vector3.left * speedPipe * Time.deltaTime;
    }
}
