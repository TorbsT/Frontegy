using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Pool<Client, Host> : IPool where Client : IPoolClient where Host : Component, IPoolHost
{
    [SerializeField] private GameObject prefab;

    public static Pool<Client, Host> Instance { get; private set; }
    private Queue<Host> queue = new Queue<Host>();

    // used for keeping track of piss and shit
    private Dictionary<Client, Host>   hostDict = new Dictionary<Client, Host>();
    private Dictionary<Host, Client> clientDict = new Dictionary<Host, Client>();
    private List<Host> allHosts = new List<Host>();

    public Pool(GameObject go)
    {
        Instance = this;
        prefab = go;
        if (prefab == null) throw new System.Exception("Poolable prefab in "+this+" is null");
    }
    public void stage(Client client)
    {
        if (client.staged) Debug.LogError("Tried connecting staged client '" + client + "'");

        if (queue.Count == 0) addObjects(1);
        Host host = queue.Dequeue();
        hostDict.Add(client, host);
        clientDict.Add(host, client);
        client.staged = true;
        allHosts.Add(host);
        host.gameObject.SetActive(true);
        host.chy = (IPoolClient)client;
        //return host;
    }
    public void unstage(Client client)
    {
        Host host = getHost(client);
        disconnect(client, host);
    }
    public void unstage(Host host)
    {
        Client client = getClient(host);
        disconnect(client, host);
    }
    public void unstageAll()
    {
        for (int i = allHosts.Count-1; i >= 0; i--)
        {  // Looping opposite direction in order to not fuck shit up
            unstage(allHosts[i]);
        }
    }
    private void disconnect(Client client, Host host)
    {
        if (!client.staged) Debug.LogError("Tried disconnecting unstaged client '"+client+"'");
        if (!host.staged) Debug.LogError("Tried disconnecting unstaged host '"+host+"'");
        queue.Enqueue(host);
        hostDict.Remove(client);
        clientDict.Remove(host);
        allHosts.Remove(host);
        client.staged = false;
        host.gameObject.SetActive(false);
    }
    public Host getHost(Client client)
    {  // O(1)
        return hostDict[client];  // throw error if not found
    }
    public Client getClient(Host host)
    {  // O(1)
        return clientDict[host];  // throw error if not found
    }
    private void addObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Object.Instantiate(prefab);
            go.SetActive(false);
            Host host = go.GetComponent<Host>();
            if (host == null) Debug.LogError(this + " uses a prefab with incorrect or no Host component");
            queue.Enqueue(host);
        }
    }
}
public class TilePool : Pool<Tile, TilePhy>  { public TilePool(GameObject go) : base(go) { } }
public class CardPool : Pool<Card, CardPhy> { public CardPool(GameObject go) : base(go) { } }
public class TroopPool : Pool<Troop, TroopPhy> { public TroopPool(GameObject go) : base(go) { } }
public class PafStepPool : Pool<PafStepChy, PafStepPhy> { public PafStepPool(GameObject go) : base(go) { } }
