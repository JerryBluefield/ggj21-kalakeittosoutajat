using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerActions : Mirror.NetworkBehaviour
{
    public int CurrentActions => currentActions;
    public bool HasHarpoon => harpoon;

    [Header("Settings")]
    [SerializeField] private int maxActions = 6;
    [SerializeField] private bool harpoon = false;
    [Header("Action costs")]
    [SerializeField] private int moveForward = 1;
    [SerializeField] private int turn = 1;
    [SerializeField] private int shootHarpoon = 2;

    [SerializeField] private int currentActions = 0;
    
    [Command]
    public void CommandEndTurn()
    {
        // This is executed on server. Below method calls RpcChangeTurn on client for the other player.
        NetworkManagerKalakeitto.Instance.EndPlayerTurn(gameObject);
    }

    [ClientRpc]
    public void RpcChangeTurn()
    {
        // This means that the other player has ended their turn.
        if (isLocalPlayer)
        {
            GameUI.Instance.StartTurn();
            ReplenishActions();
        }
    }

    private void EndTurn()
    {
        // If this does not work for both players, try to below code instead.
        GameUI.Instance.EndTurn();
        CommandEndTurn();

        // Call this when you want to end your turn.
        // Always calling CommandEndTurn does not work, so we only call it if we are not on server.
        /*if (isServer)
        {
            NetworkManagerKalakeitto.Instance.EndPlayerTurn(gameObject);
        }
        else
        {
            CommandEndTurn();
        }*/
    }

    private void Start()
    {
        //ReplenishActions();
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
            if (currentActions <= 0)
            {
                EndTurn();
            }
            return true;
        }
        GameUI.Instance.EndTurn();
        return false;
    }

    public bool MoveForwardAction()
    {
        if(currentActions >= moveForward)
        {
            currentActions -= moveForward;
            if (currentActions <= 0)
            {
                EndTurn();
            }
            return true;
        }
        GameUI.Instance.EndTurn();
        return false;
    }
}
