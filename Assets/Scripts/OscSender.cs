using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

[System.Serializable]
struct OscClientData {
    public string ip;
    public int port;
}

[System.Serializable]
struct OscClientsData {
    public OscClientData[] clients;
}

/// <summary>
/// Osc sender
/// </summary>
public class OscSender : SingletonMonoBehaviour<OscSender>
{

    [SerializeField] private OscClientsData[] _clientsData;

    private List<List<OscClient>> _clients = new List<List<OscClient>>();

    /// <summary>
    /// clients length
    /// </summary>
    /// <returns></returns>
    public int clientsLength {
        get { return this._clients.Count; }
    }

    void Awake()
    {
        foreach (OscClientsData group in this._clientsData)
        {
            this._clients.Add(new List<OscClient>());
            foreach(OscClientData data in group.clients)
            {
                this._clients[this._clients.Count-1].Add(new OscClient(data.ip, data.port));
            }
        }
    }

    /// <summary>
    /// Send
    /// </summary>
    /// <param name="index"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    public void Send(int index, string address, int data)
    {
        foreach(OscClient client in this._clients[index])
        {
            client.Send(address, data);
        }
    }

    /// <summary>
    /// Send 2 args
    /// </summary>
    /// <param name="index"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    public void Send(int index, string address, string data)
    {
        foreach(OscClient client in this._clients[index])
        {
            client.Send(address, data);
        }
    }

}
