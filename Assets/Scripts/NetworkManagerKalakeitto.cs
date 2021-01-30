using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerKalakeitto : Mirror.NetworkManager
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject childPrefab;

    private List<GameObject> connectedPlayers = new List<GameObject>();
    public Transform player1Spawn;
    public Transform player2Spawn;
    public static NetworkManagerKalakeitto Instance;

    public void EndPlayerTurn(GameObject endTurnPlayer)
    {
        foreach (GameObject player in connectedPlayers)
        {
            if (player != endTurnPlayer)
            {
                player.GetComponentInChildren<PlayerActions>().RpcChangeTurn();
            }
        }
    }

    public override void OnServerAddPlayer(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        Transform start = numPlayers == 0 ? player1Spawn : player2Spawn;
        GameObject prefab = numPlayers == 0 ? monsterPrefab : childPrefab;
        GameObject player = Instantiate(prefab, start.position, start.rotation);
        connectedPlayers.Add(player);

        Mirror.NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }

    public override void Start()
    {
        base.Start();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

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
