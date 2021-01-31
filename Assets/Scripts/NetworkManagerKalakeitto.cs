using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerKalakeitto : NetworkManager
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

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        Transform start = numPlayers == 0 ? player1Spawn : player2Spawn;
        GameObject prefab = numPlayers == 0 ? monsterPrefab : childPrefab;
        GameObject player = Instantiate(prefab, start.position, start.rotation);
        connectedPlayers.Add(player);

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("Client error " + errorCode);
        base.OnClientError(conn, errorCode);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("Client disconnect, returning to start menu");
        SceneManager.LoadScene("StartMenu");
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        Debug.Log("Hosting stopped, returning to start menu");
        SceneManager.LoadScene("StartMenu");
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
