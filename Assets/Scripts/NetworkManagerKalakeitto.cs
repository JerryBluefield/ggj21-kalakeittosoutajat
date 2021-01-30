using UnityEngine;

public class NetworkManagerKalakeitto : Mirror.NetworkManager
{
    public Transform player1Spawn;
    public Transform player2Spawn;

    public override void OnServerAddPlayer(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        Transform start = numPlayers == 0 ? player1Spawn : player2Spawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);

        Mirror.NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }
}
