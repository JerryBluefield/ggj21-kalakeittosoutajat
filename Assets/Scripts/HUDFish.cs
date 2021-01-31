using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDFish : MonoBehaviour
{
    public bool IsActive { get; private set; }
    [SerializeField] private GameObject grey;
    [SerializeField] private GameObject colored;

    public void SetActive(bool active)
    {
        grey.SetActive(!active);
        colored.SetActive(active);
        IsActive = active;
    }
}
