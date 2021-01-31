using UnityEngine;
using TMPro;
using Mirror;

public class GameUI : NetworkBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Animator turnTextAnimator;
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
