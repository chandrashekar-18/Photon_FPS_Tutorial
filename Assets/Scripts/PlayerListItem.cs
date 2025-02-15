using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameText;
    private Player _player;

    private void OnEnable()
    {
        Delegates.OnRoomLeft += OnRoomLeft;
        Delegates.OnPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable()
    {
        Delegates.OnRoomLeft -= OnRoomLeft;
        Delegates.OnPlayerLeft -= OnPlayerLeft;
    }
    
    public void SetUp(Player player)
    {
        _player = player;
        roomNameText.SetText(player.NickName);
    }
    
    private void OnRoomLeft()
    {
        Destroy(gameObject);
    }

    private void OnPlayerLeft(Player otherPlayer)
    {
        if (otherPlayer == _player)
            Destroy(gameObject);
    }
}
