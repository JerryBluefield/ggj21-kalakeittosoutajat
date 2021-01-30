using UnityEngine;
using Cinemachine;

public class NetworkManagerKalakeitto : Mirror.NetworkManager
{
    public Transform player1Spawn;
    public Transform player2Spawn;

    public CinemachineVirtualCamera virtualCamera;

    public override void OnServerAddPlayer(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        Transform start = numPlayers == 0 ? player1Spawn : player2Spawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);

        if (numPlayers == 0)
        {
            Debug.Log("Camera follows now " + player.name);
            virtualCamera.Follow = player.transform;
        }

        Mirror.NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }
}
