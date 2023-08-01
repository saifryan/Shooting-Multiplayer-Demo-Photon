using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListItemScript : MonoBehaviourPunCallbacks
{
	[SerializeField] TMP_Text text;
	Player player;

	public void SetUp(Player _player)
	{
		player = _player;
		text.text = _player.NickName;
	}

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("On Player Left Room");
        if(player == otherPlayer)
        {
			Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("On Left Room");
        Destroy(gameObject);
    }
}