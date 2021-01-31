using UnityEngine;

[ExecuteInEditMode]
public class RandomActive : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0f, 1f)] private float activeChance = 0.05f;
    [SerializeField] private bool disableInEditor;

    private void Start()
    {
        if (Application.isPlaying)
        {
            gameObject.SetActive(Random.value < activeChance);
        }

        if (!Application.isPlaying)
        {
            gameObject.SetActive(disableInEditor);
        }
    }
}
