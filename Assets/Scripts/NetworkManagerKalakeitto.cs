using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerKalakeitto : Mirror.NetworkManager
{
    public override void OnServerAddPlayer(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerAddPlayer");
        GameObject player = Instantiate(new GameObject());
        Mirror.NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(Mirror.NetworkConnection conn)
    {
        Debug.Log("Mirror.OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }
}
