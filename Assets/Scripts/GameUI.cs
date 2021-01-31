using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class GameUI : NetworkBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Animator turnTextAnimator;
    [SerializeField] private RectTransform fishContainer;
    [SerializeField] private HUDFish pickupPrototype;
    private List<HUDFish> fishes = new List<HUDFish>();
    private const string ChildTurnStartString = "The child is moving...";
    private const string MonsterTurnStartString = "The old man is moving...";
    private const string OwnTurnStartString = "My turn...";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InitializePickupCount(5);
    }

    public void InitializePickupCount(int pickupCount)
    {
        foreach (HUDFish fish in fishes)
        {
            if (fish != pickupPrototype)
            {
                Destroy(fish);
            }
        }
        pickupPrototype.SetActive(false);
        fishes.Clear();
        fishes.Add(pickupPrototype);

        for (int i = 1; i < pickupCount; i++)
        {
            HUDFish fish = Instantiate(pickupPrototype, pickupPrototype.transform.parent);
            fish.SetActive(false);
            fishes.Add(fish);
        }
    }

    public void PickupPickup()
    {
        foreach (HUDFish fish in fishes)
        {
            if (!fish.IsActive)
            {
                fish.SetActive(true);
                break;
            }
        }
    }

    public void EndTurn()
    {
        if (isServer)
        {
            turnText.text = MonsterTurnStartString;
        }
        else
        {
            turnText.text = ChildTurnStartString;
        }
        turnTextAnimator.SetTrigger("Show");
    }

    public void StartTurn()
    {
        turnText.text = OwnTurnStartString;
        turnTextAnimator.SetTrigger("Show");
    }
}
