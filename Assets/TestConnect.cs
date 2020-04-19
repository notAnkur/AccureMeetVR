using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TestConnect : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private TextMeshProUGUI debugInfo;
    [SerializeField]
    private TextMeshProUGUI pingLabel;
    [SerializeField]
    private TextMeshProUGUI usernameLabel;
    [SerializeField]
    private Button createRoomButton;
    [SerializeField]
    private Text createRoomButtonText;

    private void Awake()
    {
        createRoomButtonText.text = "Connecting...";
        createRoomButton.interactable = false;
    }

    private void Start()
    {
        Debug.Log("Connecting to Photon...", this);
        debugInfo.text = "Connecting to Photon...";
        AuthenticationValues authValues = new AuthenticationValues(System.Guid.NewGuid().ToString());
        PhotonNetwork.AuthValues = authValues;
        PhotonNetwork.SendRate = 20; //20.
        PhotonNetwork.SerializationRate = 5; //10.
        PhotonNetwork.AutomaticallySyncScene = true;
        // TODO: change nickname
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon.", this);
        debugInfo.text = "Connected to Photon.";
        Debug.Log("My nickname is " + PhotonNetwork.LocalPlayer.NickName, this);
        debugInfo.text = $"Your nickname is {PhotonNetwork.LocalPlayer.NickName}";
        //set username
        usernameLabel.text = $"Username: {PhotonNetwork.LocalPlayer.NickName.ToString()}";
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Failed to connect to Photon: " + cause.ToString(), this);
        debugInfo.text = $"Failed to connect to Photon: {cause.ToString()}!! Restart the app.";
    }

    public override void OnJoinedLobby()
    {
        print("Joined lobby");
        debugInfo.text = "Joined Lobby";
        pingLabel.text = $"Ping: {PhotonNetwork.GetPing().ToString()}ms";
        //successfully joined the lobby, enable the "Create Room Button"
        createRoomButtonText.text = "Create Room";
        createRoomButton.interactable = true;
    }
}
