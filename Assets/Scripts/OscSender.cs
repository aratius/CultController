using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

[System.Serializable]
struct OscClientData {
    public string ip;
    public int port;
}

/// <summary>
/// Osc sender
/// </summary>
public class OscSender : SingletonMonoBehaviour<OscSender>
{

    [SerializeField] private OscClientData[] _clientsData;

    private List<OscClient> _clients = new List<OscClient>();

    /// <summary>
    /// clients length
    /// </summary>
    /// <returns></returns>
    public int clientsLength {
        get { return this._clients.Count; }
    }

    void Awake()
    {
        foreach (OscClientData data in this._clientsData)
        {
            this._clients.Add(new OscClient(data.ip, data.port));
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
        this._clients[index].Send(address, data);
    }

    /// <summary>
    /// Send 2 args
    /// </summary>
    /// <param name="index"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    public void Send(int index, string address, string data)
    {
        this._clients[index].Send(address, data);
    }

}
