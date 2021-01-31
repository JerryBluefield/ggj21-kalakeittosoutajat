using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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
        mainCamera = Camera.main.transform;
        light = GetComponent<HDAdditionalLightData>();
        position = transform.position;
    }

    private void Update()
    {
        light.EnableShadows((position - mainCamera.position).sqrMagnitude < enableDistance * enableDistance);
    }
}
