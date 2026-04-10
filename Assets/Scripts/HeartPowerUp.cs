using UnityEngine;

public class HeartPowerUp : MonoBehaviour
{
    private bool canBeCollected = false;

    void Start()
    {
        Invoke(nameof(EnableCollection), 0.2f); // small delay
    }

    void EnableCollection()
    {
        canBeCollected = true;
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }
}