using UnityEngine;

public class HeartDebug : MonoBehaviour
{
    void Start()
    {
        Debug.Log("<color=green>HEART SPAWNED</color> at " + transform.position);
    }

    void OnDestroy()
    {
        // This will tell us if it's being deleted by the player or something else
        Debug.Log("<color=red>HEART DESTROYED</color>");
    }
}