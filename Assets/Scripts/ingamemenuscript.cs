using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ingamemenuscript : MonoBehaviour
{
    public GameObject header;
    public GameObject body;
    private bool active = true;

    public GameObject player;
    void Start()
    {
        header.SetActive(false);
        body.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            header.SetActive(active);
            body.SetActive(active);
            
            Cursor.visible = active;
            if (active)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                player.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                player.SetActive(true);
            }


            active = !active;
        }
    }

    public void load()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        load();
    }
}
