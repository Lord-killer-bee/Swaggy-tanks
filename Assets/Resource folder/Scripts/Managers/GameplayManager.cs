using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayManager : Photon.MonoBehaviour
{

    public Transform[] collectibleSpawnPoints;
    public GameObject[] collectibles;

    public float gameTimer = 100f;
    private bool startTimer;
    
    public List<int> collectibleBackup = new List<int>();

    public List<string> viewIDs = new List<string>();

    [HideInInspector]
    public int spawnIndex = 0;
    void Start()
    {
        Shuffle(collectibleSpawnPoints);
        collectibleBackup = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        
    }

    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(SpawnItems());
            startTimer = true;
        }
    }

    void Update()
    {
        /*if(startTimer == true)
        {
            gameTimer -= 0.02f;
            Debug.Log(gameTimer);
            if(gameTimer < 0)
            {
                startTimer = false;
            }
        }*/
        
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(4f);

        int n = Random.Range(1, 100);

        int collectibleIndex;

        

        if(n > 0 && n <= 30)
        {
            //health
            collectibleIndex = 0;

        }
        else if(n > 30 && n <= 60)
        {
            //ammo
            collectibleIndex = 1;

        }
        else if(n > 60 && n <= 85)
        {
            //nitro
            collectibleIndex = 2;

        }
        else if(n > 85 && n <= 95)
        {
            //laser
            collectibleIndex = 3;

        }
        else
        {
            //nuke
            collectibleIndex = 4;

        }

        switch (collectibleIndex)
        {
            case 0:
                GameObject pl1 = PhotonNetwork.Instantiate("health pack", collectibleSpawnPoints[collectibleBackup[spawnIndex]].position, Quaternion.identity, 0);
                //pl1.name = collectibleBackup[spawnIndex].ToString();
                pl1.GetComponent<Collectible>().NameCollectible(collectibleBackup[spawnIndex]);
                pl1.GetPhotonView().RPC("NameCollectiblePN", PhotonTargets.OthersBuffered, collectibleBackup[spawnIndex]);
                spawnIndex++;
                break;
            case 1:
                GameObject pl2 = PhotonNetwork.Instantiate("Ammo pack", collectibleSpawnPoints[collectibleBackup[spawnIndex]].position, Quaternion.Euler(-90,0,0), 0);
                //pl2.name = collectibleBackup[spawnIndex].ToString();
                pl2.GetComponent<Collectible>().NameCollectible(collectibleBackup[spawnIndex]);
                pl2.GetPhotonView().RPC("NameCollectiblePN", PhotonTargets.OthersBuffered, collectibleBackup[spawnIndex]);
                spawnIndex++;
                break;
            case 2:
                GameObject pl3 = PhotonNetwork.Instantiate("nitro", collectibleSpawnPoints[collectibleBackup[spawnIndex]].position + new Vector3(0,1,0), Quaternion.Euler(90, 0, 0), 0);
                //pl3.name = collectibleBackup[spawnIndex].ToString();
                pl3.GetComponent<Collectible>().NameCollectible(collectibleBackup[spawnIndex]);
                pl3.GetPhotonView().RPC("NameCollectiblePN", PhotonTargets.OthersBuffered, collectibleBackup[spawnIndex]);
                spawnIndex++;
                break;
            case 3:
                GameObject pl4 = PhotonNetwork.Instantiate("Laser Ammo", collectibleSpawnPoints[collectibleBackup[spawnIndex]].position + new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
                //pl4.name = collectibleBackup[spawnIndex].ToString();
                pl4.GetComponent<Collectible>().NameCollectible(collectibleBackup[spawnIndex]);
                pl4.GetPhotonView().RPC("NameCollectiblePN", PhotonTargets.OthersBuffered, collectibleBackup[spawnIndex]);
                spawnIndex++;
                break;
            case 4:
                GameObject pl5 = PhotonNetwork.Instantiate("Nuke Bomb", collectibleSpawnPoints[collectibleBackup[spawnIndex]].position, Quaternion.identity, 0);
                //pl5.name = collectibleBackup[spawnIndex].ToString();
                pl5.GetComponent<Collectible>().NameCollectible(collectibleBackup[spawnIndex]);
                pl5.GetPhotonView().RPC("NameCollectiblePN", PhotonTargets.OthersBuffered, collectibleBackup[spawnIndex]);
                spawnIndex++;
                break;
        }
        
        StartCoroutine(SpawnItems());
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

    public void SendIDtoMaster(int id)
    {
        gameObject.GetPhotonView().RPC("AddViewIDsPN", PhotonTargets.MasterClient, id);
       
    }

    [PunRPC]
    void AddViewIDsPN(int viewid)
    {
        viewIDs.Add(viewid.ToString());
    }

    [PunRPC]
	void SendIDofShell(int id)
    {
        //gets the info of the shooter
        //compare the info with the viewid array to see who killed the tank
        //then send an rpc to the tank that killed to get the kill points.
		foreach (string temp in viewIDs)
        {
			
			if(id.Equals(int.Parse(temp)))
            {
                
				PhotonView.Find(id).gameObject.GetPhotonView().RPC("AddTheKillScore", PhotonPlayer.Find(PhotonView.Find(id).ownerId));
                Debug.Log("id compared");
            }
        }
    }
}
