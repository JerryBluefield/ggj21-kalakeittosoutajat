using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[ExecuteInEditMode]
[RequireComponent(typeof(HDAdditionalLightData))]
public class DisableShadowsOnDemand : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float enableDistance;
    private Transform mainCamera;
    private HDAdditionalLightData light;
    private Vector3 position;

    private void Start()
    {
        light = GetComponent<HDAdditionalLightData>();
        if (Application.isPlaying)
        {
            mainCamera = Camera.main.transform;
            light = GetComponent<HDAdditionalLightData>();
            position = transform.position;
        }
        else
        {
            light.EnableShadows(false);
        }
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            light.EnableShadows((position - mainCamera.position).sqrMagnitude < enableDistance * enableDistance);
        }
    }
}
