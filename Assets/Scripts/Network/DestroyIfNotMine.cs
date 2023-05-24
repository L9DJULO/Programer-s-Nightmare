using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DestroyIfNotMine : MonoBehaviour
{
    public PhotonView photonView;
    public List<GameObject> DestroyGameObjects;
    public List<MonoBehaviour> DestroyScripts;
    public void Awake()
    {
        if (photonView && !photonView.IsMine)
        {
            foreach (var destroyGameObject in DestroyGameObjects)
            {
                DestroyImmediate(destroyGameObject);
            }

            foreach (var destroyScript in DestroyScripts)
            {
                DestroyImmediate(destroyScript);
            }
        }
        Destroy(this);
    }
}
