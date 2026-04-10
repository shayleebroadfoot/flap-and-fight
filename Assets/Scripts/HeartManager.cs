using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public int maxHearts = 3;
    public float spacing = 60f;

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            // Instantiate as a child of this object
            GameObject heart = Instantiate(heartPrefab, transform);

            // Set the position correctly in UI space
            RectTransform rt = heart.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * spacing, 0);

            hearts.Add(heart);
        }
    }

    public void LoseHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(lastHeart);
        }
    }

    public void AddHeart()
    {
        // Don't add more hearts than the maximum allowed
        if (hearts.Count < maxHearts)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            RectTransform rt = heart.GetComponent<RectTransform>();

            // Position it based on how many hearts we already have
            rt.anchoredPosition = new Vector2(hearts.Count * spacing, 0);

            hearts.Add(heart);
        }
    }
}
