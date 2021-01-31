using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDFish : MonoBehaviour
{
    public bool IsActive { get; private set; }
    [SerializeField] private Image grey;
    [SerializeField] private Image colored;
    [SerializeField] private Sprite greyChild;
    [SerializeField] private Sprite coloredChild;

    public void Initialize(bool isMonster)
    {
        if (isMonster)
        {
            grey.sprite = greyChild;
            colored.sprite = coloredChild;
        }
    }

    public void SetActive(bool active)
    {
        grey.gameObject.SetActive(!active);
        colored.gameObject.SetActive(active);
        IsActive = active;
    }
}
