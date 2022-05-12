using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject PlayerUI;
    public GameObject HPText;
    public GameObject SPText;
    public GameObject ItemImage;
    public Camera Cam;
    GameObject player;


    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 6;
        roomOption.CustomRoomProperties = new Hashtable() { { "키1", "문자열" }, { "키2", 1 } };

        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;
        print(CP["키1"]);
        CP["키1"] = "gd";
        CP.Add("키3", 0.5f);
        print(CP.Count);
        CP.Remove("키3");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "IsAdmin", "Admin" } });
            Hashtable playerCP = PhotonNetwork.LocalPlayer.CustomProperties;
            print(playerCP["IsAdmin"]);
        }


        DisconnectPanel.SetActive(false);
        Spawn();
    }

    public void Spawn()
    {
        player = PhotonNetwork.Instantiate("Player", new Vector3(0, 2, 0), Quaternion.identity);
        Cam.GetComponent<CameraController>().target = player;
        PlayerUI.SetActive(true);
        HPText.GetComponent<HPText>().player = player.GetComponent<PlayerController>();
        SPText.GetComponent<SPText>().player = player.GetComponent<PlayerController>();
        ItemImage.GetComponent<ItemImage>().player = player.GetComponent<PlayerController>();
        player.GetComponent<PlayerController>().cam = Cam;

    }
}