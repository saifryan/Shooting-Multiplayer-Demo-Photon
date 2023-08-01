using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LauncherScript : MonoBehaviourPunCallbacks
{
    public static LauncherScript Instance;
    public string NextSceneName = "GamePlay";
    public TMP_InputField RoomNameInputField;
    public TMP_Text ErrorText;
    public TMP_Text RoomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject StartGameButton;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public void StartGame()
    {
        Debug.Log("Start Game");
        PhotonNetwork.LoadLevel(NextSceneName);
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("On Connected To Master !.............");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!.........");
        MenuManagerScript.Instance.OpenMenu("Title");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreatedRoom()
    {
        Debug.Log("Create Room!.........");
        if(string.IsNullOrEmpty(RoomNameInputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(RoomNameInputField.text);
        MenuManagerScript.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("On Joined Room");
        MenuManagerScript.Instance.OpenMenu("Room");
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] playerlist = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < playerlist.Length; i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItemScript>().SetUp(playerlist[i]);
        }

        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("On Master Client Switched");
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorText.text = "Room Created Failed : " + message;
        MenuManagerScript.Instance.OpenMenu("Error");
    }


    public void LeaveRoom()
    {
        Debug.Log("Leave Room");
        PhotonNetwork.LeaveRoom();
        MenuManagerScript.Instance.OpenMenu("Loading");
    }

    public void JoinRoom(RoomInfo roominfo)
    {
        Debug.Log("Join Room");
        PhotonNetwork.JoinRoom(roominfo.Name);
        MenuManagerScript.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
        base.OnLeftRoom();
        MenuManagerScript.Instance.OpenMenu("Title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("On Room List Update");
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItemScript>().SetUp(roomList[i]);
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItemScript>().SetUp(newPlayer);
    }
}
