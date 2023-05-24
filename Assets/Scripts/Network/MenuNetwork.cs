using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuNetwork : MonoBehaviourPunCallbacks
{
    public int maxPlayers = 3;
    public InputField inputField_RoomNameJoin;

    public InputField inputField_RoomNameCreate;
    
    public string NameLoadLevel;
    //version du jeux en cas d'update
    private string gameVersion = "0.1";

    public void Connexion()
    {
        //pour ce connecter au lobby de la bonne version
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("lobby = join");
    }

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
            Connexion();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("lobby = connected");
        PhotonNetwork.JoinLobby();
    }
    
    //callback if creatRoom is fail
    void OnCreateRoomFailed()
    {
        Debug.Log("create fail");
    }

    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom("room"+ inputField_RoomNameCreate.text, new RoomOptions { MaxPlayers = (byte)maxPlayers },null);
        Debug.Log("room name is create, name is : room"+ inputField_RoomNameCreate.text);

    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("room"+inputField_RoomNameJoin.text);
        Debug.Log("the room is join");
    }
    
    public void OnJoinRoomFailed()
    {
        Debug.Log("join fail");
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room : " + PhotonNetwork.CurrentRoom);
        //PhotonNetwork.player.NickName = inputField_PlayerName.GetComponent<Text>().text;
        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1 && (!PhotonNetwork.OfflineMode))
        {

            // #Critical
            // Load the Room Level. 
            PhotonNetwork.LoadLevel(NameLoadLevel);

        }
    }
}
