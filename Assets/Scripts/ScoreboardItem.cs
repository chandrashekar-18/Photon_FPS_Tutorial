using System.Data;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI deathsText;
    private Player player;

    public void SetUp(Player player)
    {
        userNameText.SetText(player.NickName);
        this.player = player;
        UpdateStats();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("kills") || changedProps.ContainsKey("deaths"))
            {
                UpdateStats();
            }
        }
    }

    private void UpdateStats()
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.SetText(kills.ToString());
        }
        
        if (player.CustomProperties.TryGetValue("deaths", out object deaths))
        {
            deathsText.SetText(deaths.ToString());
        }
    }
}
