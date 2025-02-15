using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameText;
    private RoomInfo _roomInfo;

    public void SetUp(RoomInfo info)
    {
        roomNameText.SetText(info.Name);
        _roomInfo = info;
    }

    public void OnJoinClicked()
    {
        UIHandler.Instance.OpenLoadingMenu();
        PhotonManager.Instance.JoinRoom(_roomInfo);
    }
}
