using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerKalakeitto : Mirror.NetworkManager
{
    public static NetworkManagerKalakeitto Instance;
    public Transform player1Spawn;
    public Transform player2Spawn;
    private List<GameObject> players = new List<GameObject>();

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

    public void EndPlayerTurn(GameObject endingPlayer)
    {
        foreach (GameObject player in players)
        {
            if (endingPlayer != player)
            {
                player.GetComponentInChildren<PlayerActions>().RpcChangeTurn();
                break;
            }
        }
    }

    public override void OnServerAddPlayer(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        Transform start = numPlayers == 0 ? player1Spawn : player2Spawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        Mirror.NetworkServer.AddPlayerForConnection(conn, player);
        players.Add(player);
    }

    public override void OnServerDisconnect(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }

    public override void Start()
    {
        base.Start();

        if (KalakeittoStatic.isHost)
        {
            Debug.Log("Start host");
            StartHost();
        }
        else
        {
            Debug.Log("Start client");
            networkAddress = KalakeittoStatic.joinIp;
            StartClient();
        }
    }
}
