using UnityEngine;

public class ShieldDebug : MonoBehaviour
{
    void Start()
    {
        // Tells us exactly where it was born
        Debug.Log("<color=cyan>SHIELD SPAWNED</color> at: " + transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This will tell us if the player (or something else) hit it instantly
        Debug.Log("<color=yellow>Shield Triggered by:</color> " + collision.gameObject.name + " with Tag: " + collision.tag);
    }

    void OnDestroy()
    {
        // This triggers when the object is removed from the Hierarchy
        Debug.Log("<color=orange>SHIELD REMOVED FROM HIERARCHY</color>");
    }
}