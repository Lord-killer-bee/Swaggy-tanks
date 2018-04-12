using UnityEngine;
using System.Collections;

public class TankData : MonoBehaviour {
    private GameplayManager gm;
    public int myStanding = 0;
    public int currentLevel = 0;

    [HideInInspector]public float damageBuff = 0;
    private float buffAmount = 100f;

    private int tempLevel;


    void OnEnable()
    {
        gm = GameObject.Find("Gameplay manager").GetComponent<GameplayManager>();
        gm.SendIDtoMaster(gameObject.GetPhotonView().viewID);
    }

    void OnCollisionEnter(Collision target)
    {
        string name = target.gameObject.tag;

        switch (name)
        {
            case "ammo":
                myStanding = myStanding + StandingManager.instance.ammoStanding;
                break;
            case "health":
                myStanding = myStanding + StandingManager.instance.healthStanding;
                break;
            case "laser":
                myStanding = myStanding + StandingManager.instance.laserStanding;
                break;
            case "nitro":
                myStanding = myStanding + StandingManager.instance.nitroStanding;
                break;
            case "nuke bomb":
                myStanding = myStanding + StandingManager.instance.nukeStanding;
                break;
        }

    }


	
	// Update is called once per frame
	void Update () {
        CheckLevel();
	}

    void CheckLevel()
    {
        

        int n = (myStanding - StandingManager.instance.baseLevelStanding) / StandingManager.instance.standingIncrease;
        currentLevel = n + 1;
        if (currentLevel < 0) currentLevel = 0;
        tempLevel = currentLevel;
    }
    [PunRPC]
    void AddTheKillScore()
    {
        myStanding += StandingManager.instance.killStanding;
        Debug.Log("score added");
    }

}
