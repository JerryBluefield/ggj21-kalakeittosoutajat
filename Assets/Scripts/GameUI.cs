using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Animator turnTextAnimator;
    [SerializeField] private RectTransform fishContainer;
    [SerializeField] private RectTransform gameOver;
    [SerializeField] private HUDFish pickupPrototype;
    [SerializeField] private HUDFish childPickupPrototype;
    private List<HUDFish> fishes = new List<HUDFish>();
    private bool isServer;
    private bool initialized;
    private const string ChildTurnStartString = "Not your turn..";
    private const string MonsterTurnStartString = "Not your turn...";
    private const string OwnTurnStartString = "My turn...";
    private const string ChildWinString = "The child has escaped";
    private const string MonsterWinString = "The old man has won";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        fishContainer.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }


    public void InitializePickupCount(int pickupCount)
    {
        Debug.Log("init");
        fishContainer.gameObject.SetActive(true);
        isServer = true;
        pickupPrototype.Initialize(!isServer);
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
        //turnTextAnimator.SetTrigger("Show");
    }

    public void StartTurn()
    {
        turnText.text = OwnTurnStartString;
        turnTextAnimator.SetTrigger("Show");
    }

    public void EndGame(int winner)
    {
        gameOver.gameObject.SetActive(true);
        if (winner == 1)
        {
            winnerText.text = ChildWinString;
        }
        else if (winner == 2)
        {
            winnerText.text = MonsterWinString;
        }
        else
        {
            Debug.LogError("No winner??!?");
        }
    }
}
