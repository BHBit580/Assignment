using System;
using System.Collections.Generic;

[Serializable]
public class ClientDataContainer
{
    public List<Client> clients;
    public Dictionary<string, ClientData> data;
    public string label;
}
    
[Serializable]
public class ClientData
{
    public string address;
    public string name;
    public int points;
}

[Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

