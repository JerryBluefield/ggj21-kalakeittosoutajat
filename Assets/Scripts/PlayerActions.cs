using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public int CurrentActions => currentActions;

    [Header("Settings")]
    [SerializeField] private int maxActions = 6;
    [SerializeField] private bool harpoon = false;
    [Header("Action costs")]
    [SerializeField] private int moveForward = 1;
    [SerializeField] private int turn = 1;
    [SerializeField] private int shootHarpoon = 2;

    [SerializeField] private int currentActions = 0;

    // Start is called before the first frame update
    void Start()
    {
        ReplenishActions();
    }

    public void ReplenishActions()
    {
        currentActions = maxActions;
    }

    public bool TurnAction()
    {
        if (currentActions >= turn)
        {
            currentActions -= turn;
            return true;
        }
        return false;
    }

    public bool MoveForwardAction()
    {
        if(currentActions >= moveForward)
        {
            currentActions -= moveForward;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
