using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool IsCollected => isCollected;

    private bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerActions>().HasHarpoon)
        {
            gameObject.SetActive(false);
            PickUpController.Instance.PickedUp();
        }
    }

    internal List<PickUp> ToList()
    {
        throw new NotImplementedException();
    }
}
