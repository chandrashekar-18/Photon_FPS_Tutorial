using TMPro;
using Photon.Pun;
using UnityEngine;

public class UserNameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private PhotonView playerPV;

    private void Start()
    {
        if (playerPV.IsMine)
        {
            gameObject.SetActive(false);
        }
        userNameText.SetText(playerPV.Owner.NickName);
    }
}
