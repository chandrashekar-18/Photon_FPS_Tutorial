using Photon.Realtime;
using System.Collections.Generic;

public static class Delegates
{
    public delegate void LobbyJoined();
    public static LobbyJoined OnLobbyJoined;
    
    public delegate void RoomJoined(string name, Player[] players);
    public static RoomJoined OnRoomJoined;
    
    public delegate void RoomJoinFailed(string message);
    public static RoomJoinFailed OnRoomJoinFailed;

    public delegate void RoomLeft();
    public static RoomLeft OnRoomLeft;
    
    public delegate void PlayerLeft(Player player);
    public static PlayerLeft OnPlayerLeft;
    
    public delegate void PlayerEnteredRoom(Player player);
    public static PlayerEnteredRoom OnPlayerEnteredRoom;

    public delegate void RoomListUpdate(List<RoomInfo> roomList);
    public static RoomListUpdate OnRoomListUpdated;
    
    public delegate void MasterClientChanged(bool status);
    public static MasterClientChanged OnMasterClientChanged;
}
