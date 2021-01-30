using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class SetShaderPlayerPosition : MonoBehaviour
{
    [SerializeField] private VisualEffect mistEffect;

    private void Start()
    {
        mistEffect = GetComponentInChildren<VisualEffect>();
    }

    private void Update()
    {
        if (mistEffect != null)
        {
            mistEffect.SetVector3("_PlayerPosition", transform.position);
        }
    }
}
