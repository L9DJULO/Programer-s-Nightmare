using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class ConnectToServer : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("205cf3a6-8491-4636-acbc-fd731ab4fa33");
    }

    public void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); 
    }

    public void OnJoinedLobby()
    {
        SceneManager.LoadScene(5);
    }
}
