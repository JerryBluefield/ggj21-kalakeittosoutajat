using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class SetShaderPlayerPosition : Mirror.NetworkBehaviour
{
    [SerializeField] private VisualEffect mistEffect;

    private void Start()
    {
        mistEffect = GetComponentInChildren<VisualEffect>();
        if (!isLocalPlayer && mistEffect != null)
        {
            mistEffect.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isLocalPlayer && mistEffect != null)
        {
            mistEffect.SetVector3("_PlayerPosition", transform.position);
        }
    }
}
