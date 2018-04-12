using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

    public string roomName = "Room01";
    public string verName = "0.1";
    public string playerName = "Player";
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    private bool isConnected = false;



    void Start()
    {
        Shuffle(spawnPoints);
        PhotonNetwork.ConnectUsingSettings(verName);
        Debug.Log("Starting Connection");
        playerName = "Player" + Random.Range(0, 99);
    }


    public void OnJoinedLobby()
    {
        isConnected = true;
        //PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
        Debug.Log("Starting Server");
    }

    public void OnJoinedRoom()
    {

            isConnected = false;

            GameObject pl = PhotonNetwork.Instantiate(playerPrefab.name,
                                                      spawnPoints[PhotonNetwork.countOfPlayersInRooms].transform.position,
                                                      spawnPoints[PhotonNetwork.countOfPlayersInRooms].transform.rotation,
                                                      0) as GameObject;

            pl.GetComponent<TankBonusAbility>().enabled = true;
            pl.GetComponent<TankData>().enabled = true;
            pl.GetComponent<TankHealth>().enabled = true;
            pl.GetComponent<TankMovement>().enabled = true;
            pl.GetComponent<TankShooting>().enabled = true;


    }

 

    void OnGUI()
    {
        if (isConnected)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 2, Screen.height / 2));

            roomName = GUILayout.TextField(roomName);
            playerName = GUILayout.TextField(playerName);

            if (GUILayout.Button("Create"))
            {

                    PhotonNetwork.JoinOrCreateRoom(roomName, null, null);

            }

            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                if (GUILayout.Button(game.name + " " + game.playerCount + "/" + game.maxPlayers))
                {
                    PhotonNetwork.JoinOrCreateRoom(game.name, null, null);
                }
            }

            GUILayout.EndArea();
        }
    }

    void Shuffle(Transform[] arrayToShuffle)
    {
        for (int i = 0; i < arrayToShuffle.Length; i++)
        {
            //Algorithm: Loop starts at i = 0 and 'random' is assigned to any element after i.
            // then these two objects are swapped. this goes on till the end of array.
            Transform temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }
}
