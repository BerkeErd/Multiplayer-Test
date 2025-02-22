using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya katıldık!");
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); 
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon Cloud'a bağlandık. Şimdi bir odaya katılabiliriz.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Rastgele oda bulunamadı, yeni bir oda oluşturuluyor...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 }); 
    }

}
