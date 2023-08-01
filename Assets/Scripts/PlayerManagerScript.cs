using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManagerScript : MonoBehaviour
{
    PhotonView PV;
    public GameObject controller;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
            Debug.Log("is Mine");
        }
        else
        {
            Debug.Log("Not is Mine");
        }
    }

    void CreateController()
    {
        Debug.Log("Instantiated Player Controller.");
        Transform getspawnpoint = SpawnManagerScript.Instance.GetSpawnPoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), getspawnpoint.position, getspawnpoint.rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        Debug.Log("Die");
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
