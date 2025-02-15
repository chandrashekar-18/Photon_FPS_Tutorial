using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;
using Photon.Pun;
using Random = UnityEngine.Random;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI noRoomsText;
    [SerializeField] private GameObject startRoomButton;
    [SerializeField] private Transform roomContentTransform;
    [SerializeField] private GameObject roomItemPrefab;
    [SerializeField] private ScrollRect roomScrollRect;
    [SerializeField] private Transform playerContentTransform;
    [SerializeField] private GameObject playerItemPrefab;
    
    public static UIHandler Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("userName"))
        {
            string userName = PlayerPrefs.GetString("userName");
            userNameInputField.text = userName;
            PhotonNetwork.NickName = userName;
        }
        else
        {
            userNameInputField.text = "Player" + Random.Range(0, 1000).ToString("0000");
            OnUserNameInputValueChanged();
        }
    }

    private void OnEnable()
    {
        Delegates.OnLobbyJoined += OnLobbyJoined;
        Delegates.OnRoomJoined += OnRoomJoined;
        Delegates.OnRoomLeft += OnRoomLeft;
        Delegates.OnRoomJoinFailed += OnRoomJoinFailed;
        Delegates.OnRoomListUpdated += OnRoomListUpdated;
        Delegates.OnPlayerEnteredRoom += OnPlayerEnteredRoom;
        Delegates.OnMasterClientChanged += OnMasterClientChanged;
    }

    private void OnDisable()
    {
        Delegates.OnLobbyJoined -= OnLobbyJoined;
        Delegates.OnRoomJoined -= OnRoomJoined;
        Delegates.OnRoomLeft -= OnRoomLeft;
        Delegates.OnRoomJoinFailed -= OnRoomJoinFailed;
        Delegates.OnRoomListUpdated -= OnRoomListUpdated;
        Delegates.OnPlayerEnteredRoom -= OnPlayerEnteredRoom;
        Delegates.OnMasterClientChanged -= OnMasterClientChanged;
    }
    public void OpenLoadingMenu()
    {
        menuManager.OpenMenu("loading");
    }

    public void FindRoomButtonClicked()
    {
        menuManager.OpenMenu("find room");
    }

    public void CreateRoomButtonClicked()
    {
        menuManager.OpenMenu("create room");
    }
    
    public void QuitGameButtonClicked()
    {
        Application.Quit();
    }
    
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text)) return;
        PhotonManager.Instance.CreateRoom(roomNameInputField.text);
        OpenLoadingMenu();
    }
    
    public void QuitRoomButtonClicked()
    {
        PhotonManager.Instance.LeaveRoom();
        OpenLoadingMenu();
    }
    
    public void StartRoomButtonClicked()
    {
        PhotonManager.Instance.StartGame();
    }
    
    public void BackButtonClicked()
    {
        menuManager.OpenMenu("title");
    }

    public void OnUserNameInputValueChanged()
    {
        PhotonNetwork.NickName = userNameInputField.text;
        PlayerPrefs.SetString("userName", userNameInputField.text);
    }
    
    private void OnLobbyJoined()
    {
        menuManager.OpenMenu("title");
    }
    
    private void OnRoomJoined(string roomName, Player[] players)
    {
        menuManager.OpenMenu("room");
        roomNameText.SetText(roomName);
        foreach (Transform transform in playerContentTransform) Destroy(transform.gameObject);
        foreach (var player in players)
        {
            var item = Instantiate(playerItemPrefab, playerContentTransform).GetComponent<PlayerListItem>();
            item.SetUp(player);
        }
    }
    
    private void OnRoomLeft()
    {
        menuManager.OpenMenu("title");
    }
    
    private void OnRoomJoinFailed(string error)
    {
        menuManager.OpenMenu("error");
        errorText.SetText(error);
    }
    
    private void OnRoomListUpdated(List<RoomInfo> roomList)
    {
        foreach (Transform transform in roomContentTransform) Destroy(transform.gameObject);
        bool hasRooms = false;
        foreach (var room in roomList)
        {
            if (room.RemovedFromList) continue;
            hasRooms = true;
            var item = Instantiate(roomItemPrefab, roomContentTransform).GetComponent<RoomListItem>();
            item.SetUp(room);
        }
        roomScrollRect.gameObject.SetActive(hasRooms);
        noRoomsText.gameObject.SetActive(!hasRooms);
    }

    private void OnPlayerEnteredRoom(Player player)
    {
        var item = Instantiate(playerItemPrefab, playerContentTransform).GetComponent<PlayerListItem>();
        item.SetUp(player);
    }
    
    private void OnMasterClientChanged(bool status)
    {
        startRoomButton.SetActive(status);
    }
}

