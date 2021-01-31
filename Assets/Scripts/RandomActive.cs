using UnityEngine;

public class RandomActive : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0f, 1f)] private float activeChance = 0.05f;

    private void Start()
    {
        gameObject.SetActive(Random.value < activeChance);
    }
}
