using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConnectToPhotonServer();
    }
    
    private void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("[PhotonManager] :: Connecting to Photon Server...");
            PhotonNetwork.ConnectUsingSettings();
            UIHandler.Instance.OpenLoadingMenu();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("[PhotonManager] :: Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("[PhotonManager] :: Joined Lobby");
        Delegates.OnLobbyJoined?.Invoke();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[PhotonManager] :: On Room Joined");
        Delegates.OnRoomJoined?.Invoke(PhotonNetwork.CurrentRoom.Name, PhotonNetwork.PlayerList);
        Delegates.OnMasterClientChanged?.Invoke(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"[PhotonManager] :: Create Room Failed : {message}");
        Delegates.OnRoomJoinFailed?.Invoke(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("[PhotonManager] :: On Room List Updated");
        Delegates.OnRoomListUpdated?.Invoke(roomList);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"[PhotonManager] :: On Player Entered Room : {newPlayer.NickName}");
        Delegates.OnPlayerEnteredRoom?.Invoke(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"[PhotonManager] :: On Player Left : {otherPlayer.NickName}");
        Delegates.OnPlayerLeft?.Invoke(otherPlayer);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"[PhotonManager] :: On Master Client Switched : {newMasterClient.IsMasterClient}");
        Delegates.OnMasterClientChanged?.Invoke(newMasterClient.IsMasterClient);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("[PhotonManager] :: On Room Left");
        Delegates.OnRoomLeft?.Invoke();
    }
}
