using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;  // Assign your heart prefab here
    public int maxHearts = 3;       // Number of hearts
    public float spacing = 60f;     // Space between hearts

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * spacing, 0);
            hearts.Add(heart);
        }
    }

    // Call this when player takes damage
    public void LoseHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(lastHeart);
        }
    }
}