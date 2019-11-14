using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Connection connection;

#if UNITY_EDITOR
    private const string SocketUrl = "https://homeo.glitch.me/socket.io/";
    //private const string SocketUrl = "http://localhost:5000/socket.io/";
# else
    private const string SocketUrl = "https://homeo.glitch.me/socket.io/";
# endif

    private readonly Dictionary<string, ClientData> _clients = new Dictionary<string, ClientData>();

    private void Awake()
    {
        // Initialize connection
        InitConnection();
    }

    private void InitConnection()
    {
        // Setup the connection
        Debug.Log("Connecting to " + SocketUrl);
        connection = new Connection(SocketUrl, "Bouncer", "app");

        connection.OnConnect(() => { Debug.Log("Connected to server."); });

        connection.OnDisconnect(() =>
        {
            Debug.Log("Disconnected from server.");
            ClearAllClients();
        });

        connection.OnOtherConnect((id, type) =>
        {
            Debug.Log($"OTHER CONNECTED: {type} ({id})");
            if (type != "user") return;
            AddClient(id);
        });

        connection.OnOtherDisconnect((id, type) =>
        {
            Debug.Log($"OTHER DISCONNECTED: {type} ({id})");
            if (type == "user") ClearClient(id);
        });

        connection.OnError(err => { Debug.Log($"Connection error: {err}"); });

        connection.On("move", (string sourceId, float x, float y) =>
        {
            if (GetDataForClient(sourceId, out var data))
            {
                data.Input = new Vector2(x, y);
            }
        });

        connection.Open();
    }

    private void OnDestroy()
    {
        connection.Close();
    }

    private void AddClient(string id)
    {
        if (GetDataForClient(id, out var data)) data.Destroy();
        _clients[id] = new ClientData(id);
    }

    private void ClearClient(string id)
    {
        if (GetDataForClient(id, out var data)) data.Destroy();
        _clients.Remove(id);
    }

    private void ClearAllClients()
    {
        foreach (var entry in _clients)
        {
            entry.Value.Destroy();
        }

        _clients.Clear();
    }

    private bool GetDataForClient(string clientId, out ClientData data)
    {
        if (clientId == null || !_clients.ContainsKey(clientId))
        {
            data = null;
            return false;
        }

        data = _clients[clientId];
        return true;
    }
}