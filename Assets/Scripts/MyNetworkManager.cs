using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn); 

        Debug.Log("I connected to a server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log("Player was added");
        Debug.Log("Number of player on the server: " + numPlayers);
    }



}
