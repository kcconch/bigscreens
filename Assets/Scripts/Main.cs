using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Connection _connection;

#if UNITY_EDITOR
    private const string SocketUrl = "http://localhost:5000/socket.io/";
# else
    private const string SocketUrl = "http://bigscreens.herokuapp.com/socket.io/";
# endif

    private readonly Dictionary<string, ClientData> _clients = new Dictionary<string, ClientData>();
    private readonly List<int> _playerNumbers = new List<int>();

    private void Awake()
    {
        // Initialize connection
        InitConnection();
        ResetPlayerNumbers();
    }

    private void ResetPlayerNumbers()
    {
        for (var i = 0; i < 100; i++)
            _playerNumbers.Add(i + 1);
    }

    private void InitConnection()
    {
        // Setup the connection
        Debug.Log("Connecting to " + SocketUrl);
        _connection = new Connection(SocketUrl, "Bouncer", "app");

        _connection.OnConnect(() =>
        {
            Debug.Log("Connected to server.");
        });

        _connection.OnDisconnect(() =>
        {
            Debug.Log("Disconnected from server.");
            ClearAllClients();
            ResetPlayerNumbers();
        });

        _connection.OnOtherConnect((id, type) =>
       {
           Debug.Log($"OTHER CONNECTED: {type} ({id})");

           if (type == "user")
           {
               var num = _playerNumbers[0];
               _playerNumbers.RemoveAt(0);
               AddClient(id, num);
               _connection.SendTo("number", id, num);
           }

       });

        _connection.OnOtherDisconnect((id, type) =>
       {
           Debug.Log($"OTHER DISCONNECTED: {type} ({id})");
           if (type == "user") ClearClient(id);
       });

        _connection.OnError(err =>
        {
            Debug.Log($"Connection error: {err}");
        });

        _connection.On("scale", (string sourceId, float scale) =>
        {
            if (GetDataForClient(sourceId, out var data))
            {
                data.Scale = scale;
            }
        });

        _connection.On("scale", (string sourceId, float speed) =>
        {
            if (GetDataForClient(sourceId, out var data))
            {
                data.Speed = speed;
            }
        });

        _connection.On("move", (string sourceId, float x, float y) =>
        {
            if (GetDataForClient(sourceId, out var data))
            {
                data.Input = new Vector2(x, y);
            }
        });

        _connection.Open();
    }

    private void OnDestroy()
    {
        _connection.Close();
    }

    private void AddClient(string id, int num)
    {
        if (GetDataForClient(id, out var data)) data.Destroy();
        _clients[id] = new ClientData(num);
    }

    private void ClearClient(string id)
    {
        if (GetDataForClient(id, out var data))
        {
            _playerNumbers.Add(data.Number);
            data.Destroy();
        }
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




