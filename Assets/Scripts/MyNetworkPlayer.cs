using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _displayNameText = null;
    [SerializeField] private Renderer _displayColourRenderer = null;


    [SyncVar(hook=nameof(HandleDisplayNameUpdated))]
    [SerializeField] 
    private string _displayName = "Missing Name";


    [SyncVar(hook=nameof(HandleDisplayColourUpdated))]
    [SerializeField]
    private Color _displayColour = Color.black;

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        _displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColour(Color newDisplayColour)
    {
        _displayColour = newDisplayColour;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogNewName(newDisplayName);
        if(newDisplayName.Length < 2 || newDisplayName.Length > 20) { return; }

        SetDisplayName(newDisplayName);
    }


    #endregion

    #region Client

    private void HandleDisplayColourUpdated(Color oldColor, Color newColor)
    {
        _displayColourRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        _displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }


    #endregion





}
