using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Network/NetworkManagerHUD")]
[RequireComponent(typeof(Mirror.NetworkManager))]
[HelpURL("https://mirror-networking.com/docs/Articles/Components/NetworkManagerHUD.html")]
public class NetworkManagerHUDKalakeitto : MonoBehaviour
{
    Mirror.NetworkManager manager;

    public bool showGUI = true;

    public int offsetX;

    public int offsetY;

    private void Awake()
    {
        manager = GetComponent<Mirror.NetworkManager>();
    }

    void OnGUI()
    {
        if (!showGUI)
        {
            return;
        }

        GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, 215, 9999));
        if (!Mirror.NetworkClient.isConnected && !Mirror.NetworkServer.active)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
        }
    }

    void StartButtons()
    {
        if (!Mirror.NetworkClient.active)
        {
            // Server + Client
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                if (GUILayout.Button("Host (Server + Client)"))
                {
                    manager.StartHost();
                }
            }

            // Client + IP
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Client"))
            {
                manager.StartClient();
            }
            manager.networkAddress = GUILayout.TextField(manager.networkAddress);
            GUILayout.EndHorizontal();

            // Server Only
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // cant be a server in webgl build
                GUILayout.Box("(  WebGL cannot be server  )");
            }
            else
            {
                if (GUILayout.Button("Server Only")) manager.StartServer();
            }
        }
        else
        {
            // Connecting
            GUILayout.Label("Connecting to " + manager.networkAddress + "..");
            if (GUILayout.Button("Cancel Connection Attempt"))
            {
                manager.StopClient();
            }
        }
    }

    void StatusLabels()
    {
        // server / client status message
        if (Mirror.NetworkServer.active)
        {
            GUILayout.Label("Server: active. Transport: " + Mirror.Transport.activeTransport);
        }
        if (Mirror.NetworkClient.isConnected)
        {
            GUILayout.Label("Client: address=" + manager.networkAddress);
        }
    }

    void StopButtons()
    {
        // stop host if host mode
        if (Mirror.NetworkServer.active && Mirror.NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Host"))
            {
                manager.StopHost();
            }
        }
        // stop client if client-only
        else if (Mirror.NetworkClient.isConnected)
        {
            if (GUILayout.Button("Stop Client"))
            {
                manager.StopClient();
            }
        }
        // stop server if server-only
        else if (Mirror.NetworkServer.active)
        {
            if (GUILayout.Button("Stop Server"))
            {
                manager.StopServer();
            }
        }
    }
}
