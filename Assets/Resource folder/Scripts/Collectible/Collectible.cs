using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    private GameplayManager gm;

	// Use this for initialization
	void OnEnable () {
        gm = GameObject.Find("Gameplay manager").GetComponent<GameplayManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            if (PhotonNetwork.isMasterClient)
            {
                gm.collectibleBackup.Add(int.Parse(gameObject.name));
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void NameCollectible(int index)
    {
        gameObject.name = gm.collectibleBackup[index].ToString();
    }

    [PunRPC]
    public void NameCollectiblePN(int index)
    {
        gameObject.name = gm.collectibleBackup[index].ToString();
    }

}
