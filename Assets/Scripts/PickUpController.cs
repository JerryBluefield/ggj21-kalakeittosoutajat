using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpController : Mirror.NetworkBehaviour
{
    public static PickUpController Instance;


    [Header("Component Reference")]
    [SerializeField] private Level currentLevel;
    [SerializeField] private List<PickUp> pickUps;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pickUps = currentLevel.PickUpsParent.GetComponentsInChildren<PickUp>().ToList();
        GameUI.Instance.InitializePickupCount(pickUps.Count);
        Debug.Log("Pickup count: " + pickUps.Count);
    }

    public void PickedUp()
    {
        GameUI.Instance.PickupPickup();
        int remainingPickUps = 0;
        for (int i = 0; i < pickUps.Count; i++)
        {
            if (pickUps[i].gameObject.activeSelf)
            {
                remainingPickUps++;
            }
        }

        Debug.Log("Remaining PickUps: " + remainingPickUps);

        if(remainingPickUps == 0)
        {
            Debug.Log("Child wins!");
            //TODO: Child wins game.
        }

    }

    // Call when changing turns in order for the child to see the pickups but the fisherman should not see them.
    public void SetPickUpState()
    {
        bool pickUpEnabled = true;
        if (PlayerController.Instance.CurrentPlayer.GetComponent<PlayerActions>().HasHarpoon)
        {
            pickUpEnabled = false;
        }

        for (int i = 0; i < pickUps.Count; i++)
        {
            if (!pickUps[i].IsCollected)
            {
                pickUps[i].gameObject.SetActive(pickUpEnabled);
            }

        }
    }
}
